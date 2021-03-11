using AO.Models.Static;
using Dapper;
using Dapper.CX.SqlServer;
using DataTables.Library;
using Microsoft.Data.SqlClient;
using SqlIntegration.Library;
using SqlIntegration.Library.Classes;
using SqlIntegration.Library.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Zinger.Models;

namespace Zinger.Services
{
    public class DataMigrator
    {
        private readonly SavedConnections _savedConnections;
        
        private CancellationTokenSource _cts;

        public DataMigrator(SavedConnections savedConnections)
        {
            _savedConnections = savedConnections;        
        }

        public string CurrentFilename { get; set; }

        public event EventHandler<SqlMigrator<int>.Progress> Progress;

        public Dictionary<string, object> KeyViolationValues { get; private set; }

        /// <summary>
        /// initialize a Step object by comparing columns from a source and destination table,
        /// </summary>
        public async Task<DataMigration.Step> CreateStepAsync(
            string sourceConnection, string destConnection, string fromWhere, string destTable, object parameters = null)
        {
            DataMigration.Step step = new DataMigration.Step()
            {
                SourceFromWhere = fromWhere,
                DestTable = destTable
            };
            
            await AddStepColumnsAsync(sourceConnection, destConnection, step, parameters);

            return step;
        }

        public async Task AddStepColumnsAsync(DataMigration migration, DataMigration.Step step) =>
            await AddStepColumnsAsync(migration.SourceConnection, migration.DestConnection, step, migration.GetParameters());
        
        public async Task AddStepColumnsAsync(
            string sourceConnection, string destConnection, DataMigration.Step step, object parameters = null)
        {
            List<DataMigration.Column> result = new List<DataMigration.Column>();            

            await ExecuteWithConnectionsAsync(sourceConnection, destConnection, async (source, dest) =>
            {
                var schemaCols = await GetStepSchemaColumns(source, dest, step, parameters);

                step.SourceIdentityColumn = findIdentityColumn(schemaCols.sourceColumns);
                step.DestIdentityColumn = findIdentityColumn(schemaCols.destColumns);

                // add matching columns
                var columns = (from src in nonIdentityColumns(schemaCols.sourceColumns)
                               join dst in nonIdentityColumns(schemaCols.destColumns) on ColumnName(src) equals ColumnName(dst)
                               select new DataMigration.Column()
                               {
                                   Source = ColumnName(src),
                                   Dest = ColumnName(dst)
                               }).ToList();

                var notInDest = nonIdentityColumns(schemaCols.sourceColumns).Select(row => ColumnName(row)).Except(nonIdentityColumns(schemaCols.destColumns).Select(row => ColumnName(row)));
                columns.AddRange(notInDest.Select(col => new DataMigration.Column()
                {
                    Source = col
                }));

                var notInSrc = nonIdentityColumns(schemaCols.destColumns).Select(row => ColumnName(row)).Except(nonIdentityColumns(schemaCols.sourceColumns).Select(row => ColumnName(row)));
                columns.AddRange(notInSrc.Select(col => new DataMigration.Column()
                {
                    Dest = col
                }));

                result.AddRange(columns);
            });

            step.Columns = result;            

            IEnumerable<DataRow> nonIdentityColumns(DataTable schemaTable) => schemaTable.AsEnumerable().Where(row => !IsIdentity(row));

            string findIdentityColumn(DataTable schemaTable)
            {
                try
                {
                    return ColumnName(schemaTable.AsEnumerable().First(row => IsIdentity(row)));
                }
                catch 
                {
                    return null;
                }                
            }
        }

        private string ColumnName(DataRow row) => row.Field<string>("ColumnName");

        private bool IsRequired(DataRow row) => !row.Field<bool>("AllowDbNull");

        private async Task<(DataTable sourceColumns, DataTable destColumns)> GetStepSchemaColumns(SqlConnection source, SqlConnection dest, DataMigration.Step step, object parameters = null)
        {
            var sourceCols = await GetSchemaColumns(source, $"SELECT * FROM {step.SourceFromWhere}", parameters);
            var destCols = await GetSchemaColumns(dest, $"SELECT * FROM {step.DestTable}", parameters);

            return (sourceCols, destCols);
        }

        private async Task<DataTable> GetSchemaColumns(SqlConnection connection, string sql, object parameters = null)
        {
            try
            {
                return await connection.QuerySchemaTableAsync(sql, parameters);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error getting schema columns: {exc.Message} from query: {sql}");
            }
        }

        private bool IsIdentity(DataRow row) => row.Field<bool>("IsIdentity");

        private async Task ExecuteWithConnectionsAsync(DataMigration migration, Func<SqlConnection, SqlConnection, Task> execute) =>
            await ExecuteWithConnectionsAsync(migration.SourceConnection, migration.DestConnection, execute);

        private async Task ExecuteWithConnectionsAsync(string sourceConnection, string destConnection, Func<SqlConnection, SqlConnection, Task> execute)
        {
            var allConnections = _savedConnections.Connections.ToDictionary(item => item.Name, item => item.ConnectionString);

            using (var cnSource = new SqlConnection(allConnections[sourceConnection]))
            {
                cnSource.Open();
                using (var cnDest = new SqlConnection(allConnections[destConnection]))
                {
                    cnDest.Open();
                    await execute.Invoke(cnSource, cnDest);
                }
            }
        }

        private Dictionary<string, string> GetForeignKeyMapping(DataMigration.Step step) =>
            step.Columns
                .Where(col => !string.IsNullOrEmpty(col.KeyMapTable) && !col.KeyMapTable.StartsWith("@"))
                .ToDictionary(col => col.Dest, col => col.KeyMapTable);        

        private async Task<(DataTable table, string sql)> QuerySourceTableAsync(SqlConnection cnSource, DataMigration.Step step, object parameters = null)
        {
            const string columnToken = "{columns}";

            var columnList = GetStepColumnList(step);

            var query = (step.SourceFromWhere.Contains(columnToken)) ?
                step.SourceFromWhere.Replace(columnToken, columnList) :
                $"SELECT {columnList} FROM {step.SourceFromWhere}";

            try
            {
                var dataTable = await cnSource.QueryTableAsync(query, parameters);
                return (dataTable, query);
            }
            catch (Exception exc)
            {
                throw new QueryException(query, exc);
            }
        }

        private string GetStepColumnList(DataMigration.Step step, string sourceIdAlias = null)
        {
            var columns = string.Join(", ", step.Columns
                .Where(col => !string.IsNullOrWhiteSpace(col.Source) && !string.IsNullOrWhiteSpace(col.Dest))
                .Select(col => (col.Source.StartsWith("=")) ?
                    $"{col.Source.Substring(1)} AS [{col.Dest}]" :
                    $"{SqlBuilder.ApplyDelimiter(col.Source, '[', ']')} AS [{col.Dest}]"));

            string sourceId = SqlBuilder.ApplyDelimiter(step.SourceIdentityColumn, '[', ']');

            if (!string.IsNullOrEmpty(sourceIdAlias))
            {
                sourceId += $" AS [{sourceIdAlias}]";
            }

            return $"{columns}, {sourceId}";
        }

        /// <summary>
        /// copies the key map table from the dest connection to the source
        /// so you can do inspect and troubleshoot the mapping in the source connection
        /// </summary>
        public async Task<(DbObject @object, int rowCount)> ImportKeyMapTableAsync(DataMigration dataMigration)
        {
            DbObject result = null;
            int rowCount = 0;

            await ExecuteWithConnectionsAsync(dataMigration, async (source, dest) =>
            {
                var migrator = await GetMigratorAsync(dest);
                result = SqlMigrator<int>.KeyMapTable;

                if (await source.TableExistsAsync(result))
                {
                    await source.ExecuteAsync($"DROP TABLE [{result.Schema}].[{result.Name}]");
                }

                if (!await source.SchemaExistsAsync(result.Schema))
                {
                    await source.ExecuteAsync($"CREATE SCHEMA [{result.Schema}]");
                }

                string query = $"SELECT * FROM [{result.Schema}].[{result.Name}]";
                var data = await dest.QueryTableAsync(query);
                rowCount = data.Rows.Count;
                var createTable = await dest.SqlCreateTableAsync(result.Schema, result.Name, query);
                await source.ExecuteAsync(createTable);

                await BulkInsert.ExecuteAsync(data, source, result, 35, new BulkInsertOptions()
                {
                    IdentityInsert = true
                });
            });

            return (result, rowCount);
        }

        public async Task AddColumnsAsync(string fileName, bool overwrite = false, object parameters = null)
        {
            var json = File.ReadAllText(fileName);
            var migration = JsonSerializer.Deserialize<DataMigration>(json);

            foreach (var step in migration.Steps)
            {
                if (overwrite || (!step.Columns?.Any() ?? true))
                {
                    await AddStepColumnsAsync(migration.SourceConnection, migration.DestConnection, step, parameters);
                }
            }

            json = JsonSerializer.Serialize(migration, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, json);
        }

        public void Cancel()
        {
            _cts?.Cancel();
        }

        private async Task<SqlMigrator<int>> GetMigratorAsync(SqlConnection dest)
        {
            var result = await SqlMigrator<int>.InitializeAsync(dest);

            // ignore PK violations, but throw all others
            result.OnInsertException = AnalyzeInsertExceptionAsync;

            // in AH4 when mapping from dbo.Customer, if the sourceId is negative, it's being used as a Folder.ParentId.
            // I didn't have a way to do this delcaratively within the migration model, so it's hardcoded into the migrator.
            result.OnMappingException = async (exc, cn, obj, sourceId, txn) =>
            {
                if (sourceId < 0 && obj.Equals(DbObject.Parse("dbo.Customer")))
                {
                    return await result.GetNewIdAsync(cn, obj, sourceId * -1, txn);
                }

                return default;
            };

            result.OnProgress += delegate (SqlMigrator<int>.Progress progress)
            {
                Progress?.Invoke(this, progress);
            };

            return result;
        }

        private async Task<bool> AnalyzeInsertExceptionAsync(
            SqlConnection connection, InsertExceptionType type, DataRow dataRow, Exception exception, Dictionary<string, object> values)
        {
            KeyViolationValues = values;
            return await Task.FromResult(exception.Message.Contains("duplicate key"));
        }

        private Dictionary<string, Dictionary<int, int>> GetInlineMappings(DataMigration.Step step)
        {
            try
            {
                var inlineMappedCols = step.Columns
                    .Where(col => col.KeyMapTable?.StartsWith("@") ?? false && !string.IsNullOrEmpty(col.Dest))
                    .Select(col => new
                    {
                        Column = col,
                        MappingFile = Path.Combine(Path.GetDirectoryName(CurrentFilename), col.KeyMapTable.Substring(1))
                    });

                return inlineMappedCols.Select(colInfo =>
                {
                    var json = File.ReadAllText(colInfo.MappingFile);
                    var map = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
                    return new
                    {
                        ColumnName = colInfo.Column.Dest,
                        Mappings = map.Select(kp => new KeyValuePair<int, int>(int.Parse(kp.Key), kp.Value)).ToDictionary(item => item.Key, item => item.Value)
                    };
                }).ToDictionary(item => item.ColumnName, item => item.Mappings);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error getting inline mappings: {exc.Message}");
            }
        }

        /// <summary>
        /// use this for mapping enum values that are inconvenient to use with a table.
        /// Create a json file in the same folder as the migration you loaded that can deserialize to Dictionary string, int
        /// </summary>
        private void ApplyInlineMapping(DataMigration.Step step, SqlServerCmd cmd, DataRow row, Dictionary<string, Dictionary<int, int>> inlineMappings)
        {
            try
            {
                foreach (var col in inlineMappings)
                {
                    if (!row.IsNull(col.Key))
                    {
                        var key = row.Field<int>(col.Key);
                        var value = col.Value[key];
                        cmd[col.Key] = value;
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception($"Error using inline mapping: {exc.Message}");
            }
        }

        public async Task<MigrationResult> RunStepAsync(DataMigration.Step step, DataMigration migration, int maxRows = 0)
        {
            var result = new MigrationResult();
            result.Action = "copied";

            await ExecuteWithConnectionsAsync(migration, async (source, dest) =>
            {
                try
                {
                    var sourceData = await QuerySourceTableAsync(source, step, migration.GetParameters());
                    result.SourceSql = sourceData.sql;
                    var migrator = await GetMigratorAsync(dest);
                    await RunStepInnerAsync(step, maxRows, dest, migrator, result, sourceData.table);
                }
                catch (QueryException exc)
                {
                    result.Success = false;
                    result.SourceSql = exc.Sql;
                    result.Message = exc.Message;
                }
            });

            return result;
        }

        /// <summary>
        /// runs a step then rolls it back, collecting any error messages and SQL artifacts that result
        /// </summary>
        public async Task<MigrationResult> ValidateStepAsync(DataMigration.Step step, DataMigration migration, int maxRows = 10)
        {            
            var result = new MigrationResult();
            result.Action = "tested";

            await ExecuteWithConnectionsAsync(migration, async (source, dest) =>
            {
                try
                {
                    var sourceData = await QuerySourceTableAsync(source, step, migration.GetParameters());
                    result.SourceSql = sourceData.sql;
                    var migrator = await GetMigratorAsync(dest);

                    using (var txn = dest.BeginTransaction())
                    {
                        try
                        {
                            await RunStepInnerAsync(step, maxRows, dest, migrator, result, sourceData.table, txn);
                        }
                        finally
                        {
                            txn.Rollback();
                        }
                    }
                }
                catch (QueryException exc)
                {
                    result.Success = false;
                    result.SourceSql = exc.Sql;
                    result.Message = exc.Message;
                }
            });

            return result;
        }

        public async Task<MappingProgress> QueryMappingProgressAsync(DataMigration migration, DataMigration.Step step)
        {
            MappingProgress result = null;

            await ExecuteWithConnectionsAsync(migration, async (source, dest) =>
            {                
                var sql = GetProgressQuery(step);
                var table = (await source.QueryTableAsync(sql, migration.GetParameters())).AsEnumerable().Select(row => new MappingProgress(row));
                result = table.FirstOrDefault();                
            });

            return result;
        }

        private string GetProgressQuery(DataMigration.Step step)
        {
            var source = ParseSource(step.SourceFromWhere);

            var destTable = DbObject.Parse(step.DestTable);
            var keymapTable = SqlMigrator<int>.KeyMapTable;

            return 
                $@"WITH {source.cte}[inner] AS (
                    SELECT 
                        [SourceId],
                        [NewId]
                    FROM (
		                SELECT {SqlBuilder.ApplyDelimiter(step.SourceIdentityColumn, '[', ']')} AS [Id]
		                FROM {source.body}
	                ) AS [source]
                    LEFT JOIN (
                        SELECT [km].[SourceId], [km].[NewId]
                        FROM [{keymapTable.Schema}].[{keymapTable.Name}] [km]
		                WHERE [km].[Schema]='{destTable.Schema}' AND [km].[TableName]='{destTable.Name}'
                    ) [map] ON [source].[Id]=[map].[SourceId]
                ), [source_total] AS (
                    SELECT COUNT(1) AS [SourceRows] FROM [inner]
                ), [unmapped] AS (
                    SELECT COUNT(1) AS [UnmappedRows] FROM [inner] WHERE [NewId] IS NULL
                ), [mapped] AS (
                    SELECT COUNT(1) AS [MappedRows] FROM [inner] WHERE [SourceId] IS NOT NULL
                ) SELECT
                    {step.Order} AS [Order],
                    '{destTable.Schema}' AS [Schema],
                    '{destTable.Name}' AS [Name],
                    [source_total].[SourceRows],
                    [mapped].[MappedRows],
                    [unmapped].[UnmappedRows]
                FROM
                    [mapped],
                    [unmapped],
                    [source_total]";
        }

        public string GetUnmappedRowsQuery(DataMigration.Step step)
        {
            var source = ParseSource(step.SourceFromWhere);

            var destTable = DbObject.Parse(step.DestTable);
            var keymapTable = SqlMigrator<int>.KeyMapTable;

            const string sourceIdAlias = "_src_id";

            return
                $@"WITH {source.cte}[source] AS (
                    SELECT
                        {GetStepColumnList(step, sourceIdAlias)}
                    FROM 
                        {source.body}                    
                ) SELECT 
                    [source].*
                FROM 
                    [source]
                WHERE 
                    NOT EXISTS(
                        SELECT 1 FROM [{keymapTable.Schema}].[{keymapTable.Name}] [km]
                        WHERE 
                            [km].[Schema]='{destTable.Schema}' AND 
                            [km].[TableName]='{destTable.Name}' AND
                            [km].[SourceId]=[source].[{sourceIdAlias}]
                        )";
        }

        private static (string cte, string body) ParseSource(string sourceFromWhere)
        {
            var customQueryToken = Regex.Match(sourceFromWhere, @"SELECT(\s*){columns}(\s*)FROM");

            string body = (customQueryToken.Success) ?
                sourceFromWhere.Substring(customQueryToken.Index + customQueryToken.Length) :
                sourceFromWhere;

            string cte = (customQueryToken.Success) ?
                sourceFromWhere.Substring("WITH ".Length, customQueryToken.Index - "WITH ".Length) + ", " :
                string.Empty;

            return (cte, body);
        }      

        private async Task RunStepInnerAsync(
            DataMigration.Step step, int maxRows, 
            SqlConnection dest, SqlMigrator<int> migrator, MigrationResult result, 
            DataTable table, SqlTransaction txn = null)
        {            
            var intoTable = DbObject.Parse(step.DestTable);
            var mappings = GetForeignKeyMapping(step);
            var inlineMappings = GetInlineMappings(step);
            
            _cts = new CancellationTokenSource();

            try
            {
                result.RowsCopied = await migrator.CopyRowsAsync(
                    dest, table, step.DestIdentityColumn, intoTable.Schema, intoTable.Name, _cts,
                    mappings, onEachRow: (cmd, row) => ApplyInlineMapping(step, cmd, row, inlineMappings), 
                    txn: txn, maxRows: maxRows);
                result.Success = true;
                result.Message = "Step succeeded.";
                result.InsertSql = migrator.MigrateCommand.GetInsertStatement();
            }
            catch (Exception exc)
            {
                result.Message = exc.Message;
            }
        }

        private class ValidationMessage
        {
            /// <summary>
            /// DataMigration.Column.Key or "_step"
            /// </summary>
            public string Context { get; set; }
            public string Message { get; set; }
        }

        public class MigrationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string SourceSql { get; set; }
            public string InsertSql { get; set; }            
            public int RowsCopied { get; set; }
            public int RowsSkipped { get; set; }
            public string Action { get; set; }            
        }

        public class MappingProgress
        {
            public MappingProgress()
            {
            }

            public MappingProgress(DataRow row)
            {
                Order = row.Field<int>("Order");
                Schema = row.Field<string>("Schema");
                Name = row.Field<string>("Name");
                SourceRows = row.Field<int>("SourceRows");
                MappedRows = row.Field<int>("MappedRows");
                UnmappedRows = row.Field<int>("UnmappedRows");
            }

            [Browsable(false)]
            public int Order { get; set; }

            [Category("DbObject")]
            public string Schema { get; set; }
            [Category("DbObject")]
            public string Name { get; set; }
            /// <summary>
            /// number of rows in the migrate.KeyMap table for this DestTable
            /// </summary>
            [Category("Rows")]
            public int MappedRows { get; set; }
            /// <summary>
            /// number of rows that don't have a NewId
            /// </summary>
            [Category("Rows")]
            public int UnmappedRows { get; set; }
            /// <summary>
            /// total number of rows in the source query
            /// </summary>
            [Category("Rows")]
            public int SourceRows { get; set; }
        }

        public class QueryException : Exception
        {
            public QueryException(string sql, Exception innerException) : base(innerException.Message, innerException)
            {
                Sql = sql;
            }

            public string Sql { get; }
        }
    }    
}

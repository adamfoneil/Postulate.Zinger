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
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Zinger.Models;

namespace Zinger.Services
{
    public class DataMigrator
    {
        private readonly SavedConnections _savedConnections;        

        public DataMigrator(SavedConnections savedConnections)
        {
            _savedConnections = savedConnections;            
        }

        /// <summary>
        /// built-in actions we can take on rows before they get migrated.
        /// This is because I don't know how to build expressions dynamically from text,
        /// and all I really need is the ability to invert some Id values for root level AH folders
        /// </summary>
        private Dictionary<string, Action<SqlServerCmd, string, DataRow>> Transforms => new Dictionary<string, Action<SqlServerCmd, string, DataRow>>()
        {
            ["invert"] = (cmd, col, row) => cmd[col] = Convert.ToInt32(row[col]) * -1
        };

        public string CurrentFilename { get; set; }

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
                .ToDictionary(col => col.Dest, col => ParseKeyMapExpression(col.KeyMapTable).tableName);        

        /// <summary>
        /// parses a table name and expression from a DataMigration.Step.Column.KeyMapTable
        /// </summary>
        private (string tableName, string transform) ParseKeyMapExpression(string keyMapExpression)
        {
            if (string.IsNullOrEmpty(keyMapExpression)) return (null, null);

            const string separator = "=>";

            int separatorIndex = keyMapExpression.IndexOf(separator);
            return (separatorIndex < 0) ?
                (keyMapExpression.Trim(), null) :
                (keyMapExpression.Substring(0, separatorIndex).Trim(), keyMapExpression.Substring(separatorIndex + separator.Length).Trim());
        }

        private async Task<(DataTable table, string sql)> QuerySourceTableAsync(SqlConnection cnSource, DataMigration.Step step, object parameters = null)
        {
            var columns = string.Join(", ", step.Columns
                .Where(col => !string.IsNullOrWhiteSpace(col.Source) && !string.IsNullOrWhiteSpace(col.Dest))
                .Select(col => (col.Source.StartsWith("=")) ? 
                    $"{col.Source.Substring(1)} AS [{col.Dest}]" : 
                    $"{SqlBuilder.ApplyDelimiter(col.Source, '[', ']')} AS [{col.Dest}]"));

            var query = $"SELECT {columns}, {SqlBuilder.ApplyDelimiter(step.SourceIdentityColumn, '[', ']')} FROM {step.SourceFromWhere}";

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
                result = migrator.KeyMapTable;

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

                await BulkInsert.ExecuteAsync(data, source, result, 30, new BulkInsertOptions()
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

        private Dictionary<string, Action<SqlServerCmd, string, DataRow>> GetTransforms(DataMigration.Step step) =>
            step.Columns
                .Where(col => !string.IsNullOrEmpty(ParseKeyMapExpression(col.KeyMapTable).transform) && !string.IsNullOrEmpty(col.Dest))
                .ToDictionary(col => col.Dest, col => Transforms[ParseKeyMapExpression(col.KeyMapTable).transform]);                

        private async Task<SqlMigrator<int>> GetMigratorAsync(SqlConnection dest)
        {
            var result = await SqlMigrator<int>.InitializeAsync(dest);

            // ignore PK violations, but throw all others
            result.OnInsertException = async (cn, dataRow, exc) => await Task.FromResult(exc.Message.Contains("duplicate key"));

            result.OnMappingException = async (exc, cn, obj, sourceId, txn) => 
            {                
                if (sourceId < 0 && obj.Equals(DbObject.Parse("dbo.Customer")))
                {
                    return await result.GetNewIdAsync(cn, obj, sourceId * -1, txn);
                }

                return default;
            };

            return result;
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

        private async Task RunStepInnerAsync(
            DataMigration.Step step, int maxRows, 
            SqlConnection dest, SqlMigrator<int> migrator, MigrationResult result, 
            DataTable table, SqlTransaction txn = null)
        {            
            var intoTable = DbObject.Parse(step.DestTable);
            var mappings = GetForeignKeyMapping(step);
            var inlineMappings = GetInlineMappings(step);
            var transforms = GetTransforms(step);

            try
            {
                result.RowsCopied = await migrator.CopyRowsAsync(
                    dest, table, step.DestIdentityColumn, intoTable.Schema, intoTable.Name,
                    mappings, onEachRow: (cmd, row) =>
                    {
                        //ApplyTransforms(cmd, row, transforms);
                        ApplyInlineMapping(step, cmd, row, inlineMappings);
                    }, txn: txn, maxRows: maxRows);
                result.Success = true;
                result.Message = "Step succeeded.";
                result.InsertSql = migrator.MigrateCommand.GetInsertStatement();
            }
            catch (Exception exc)
            {
                result.Message = exc.Message;
            }
        }

        private void ApplyTransforms(SqlServerCmd cmd, DataRow row, Dictionary<string, Action<SqlServerCmd, string, DataRow>> transforms)
        {
            foreach (var t in transforms)
            {
                t.Value.Invoke(cmd, t.Key, row);
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

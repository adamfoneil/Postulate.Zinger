using AO.Models.Static;
using Dapper.CX.SqlServer;
using DataTables.Library;
using Microsoft.Data.SqlClient;
using SqlIntegration.Library;
using SqlSchema.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
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
            
            await BuildColumnsAsync(sourceConnection, destConnection, step, parameters);

            return step;
        }

        public async Task BuildColumnsAsync(
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
            var sourceCols = await GetSchemaColumns(source, step.SourceFromWhere, parameters);
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

        public async Task ExecuteAsync(string fileName, CancellationTokenSource cts, object parameters = null)
        {
            var json = File.ReadAllText(fileName);
            var migration = JsonSerializer.Deserialize<DataMigration>(json);
            await ExecuteAsync(migration, cts, parameters);
        }

        public async Task ExecuteAsync(DataMigration migration, CancellationTokenSource cts, object parameters = null)
        {
            await ExecuteWithConnectionsAsync(migration.SourceConnection, migration.DestConnection, async (source, dest) =>
            {
                var migrator = await SqlMigrator<int>.InitializeAsync(dest);

                foreach (var step in migration.Steps.OrderBy(s => s.Order))
                {
                    if (cts.IsCancellationRequested) break;

                    var sourceTable = await QuerySourceTableAsync(source, step, parameters);
                    var dbobj = DbObject.Parse(step.DestTable);
                    var fkmapping = GetForeignKeyMapping(step);
                    await migrator.CopyRowsAsync(dest, sourceTable.table, step.SourceIdentityColumn, dbobj.Schema, dbobj.Name, fkmapping);                    
                }
            });
        }

        private Dictionary<string, string> GetForeignKeyMapping(DataMigration.Step step) =>
            step.Columns
                .Where(col => !string.IsNullOrEmpty(col.KeyMapTable) && !col.KeyMapTable.StartsWith("@"))
                .ToDictionary(col => col.Dest, col => col.KeyMapTable);
        
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

        public async Task AddColumnsAsync(string fileName, bool overwrite = false, object parameters = null)
        {
            var json = File.ReadAllText(fileName);
            var migration = JsonSerializer.Deserialize<DataMigration>(json);

            foreach (var step in migration.Steps)
            {
                if (overwrite || (!step.Columns?.Any() ?? true))
                {
                    await BuildColumnsAsync(migration.SourceConnection, migration.DestConnection, step, parameters);
                }
            }

            json = JsonSerializer.Serialize(migration, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        /// runs a step then rolls it back, collecting any error messages and SQL artifacts that result
        /// </summary>
        public async Task<MigrationResult> ValidateStepAsync(DataMigration.Step step, DataMigration migration, int maxRows = 10)
        {
            var result = new MigrationResult();

            await ExecuteWithConnectionsAsync(migration, async (source, dest) =>
            {
                try
                {
                    var sourceData = await QuerySourceTableAsync(source, step, migration.GetParameters());
                    result.SourceSql = sourceData.sql;
                    result.Action = "tested";

                    var migrator = await GetMigratorAsync(dest);
                    var intoTable = DbObject.Parse(step.DestTable);
                    var mappings = GetForeignKeyMapping(step);
                    var inlineMappings = GetInlineMappings(step);

                    using (var txn = dest.BeginTransaction())
                    {
                        try
                        {
                            await migrator.CopyRowsAsync(dest,
                                sourceData.table, step.DestIdentityColumn, intoTable.Schema, intoTable.Name,
                                mappings, onEachRow: (cmd, row) => ApplyInlineMapping(step, cmd, row, inlineMappings), txn: txn, maxRows: maxRows);
                            result.Success = true;
                            result.Message = "Step succeeded.";
                            result.InsertSql = migrator.MigrateCommand.GetInsertStatement();
                        }
                        catch (Exception exc)
                        {
                            result.Message = exc.Message;
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

        private async Task<SqlMigrator<int>> GetMigratorAsync(SqlConnection dest)
        {
            var result = await SqlMigrator<int>.InitializeAsync(dest);

            // I'm just ignoring all errors
            result.OnInsertException = async (cn, dataRow, exc) => await Task.FromResult(true);

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
                    var intoTable = DbObject.Parse(step.DestTable);
                    var mappings = GetForeignKeyMapping(step);
                    var inlineMappings = GetInlineMappings(step);

                    try
                    {
                        result.RowsCopied = await migrator.CopyRowsAsync(
                            dest, sourceData.table, step.DestIdentityColumn, intoTable.Schema, intoTable.Name,
                            mappings, onEachRow: (cmd, row) => ApplyInlineMapping(step, cmd, row, inlineMappings), maxRows: maxRows);
                        result.Success = true;
                        result.Message = "Step succeeded.";
                        result.InsertSql = migrator.MigrateCommand.GetInsertStatement();
                    }
                    catch (Exception exc)
                    {
                        result.Message = exc.Message;
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

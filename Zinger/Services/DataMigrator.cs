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
                .Where(col => !string.IsNullOrEmpty(col.KeyMapTable))
                .ToDictionary(col => col.Source, col => col.KeyMapTable);
        
        private async Task<(DataTable table, string sql)> QuerySourceTableAsync(SqlConnection cnSource, DataMigration.Step step, object parameters = null)
        {
            var columns = string.Join(", ", step.Columns
                .Where(col => !string.IsNullOrWhiteSpace(col.Source) && !string.IsNullOrWhiteSpace(col.Dest))
                .Select(col => (col.Source.StartsWith("=")) ? 
                    $"{col.Source.Substring(1)} AS [{col.Dest}]" : 
                    $"[{col.Source}] AS [{col.Dest}]"));

            var query = $"SELECT {columns}, [{step.SourceIdentityColumn}] FROM {step.SourceFromWhere}";

            var dataTable = await cnSource.QueryTableAsync(query, parameters);
            return (dataTable, query);
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
        public async Task<(bool success, string message, string sourceSql, string insertSql)> ValidateStepAsync(DataMigration.Step step, DataMigration migration, int maxRows = 10)
        {
            bool success = false;
            string message = null;
            string sourceSql = null;
            string insertSql = null;

            await ExecuteWithConnectionsAsync(migration, async (source, dest) =>
            {
                var sourceData = await QuerySourceTableAsync(source, step, migration.GetParameters());
                sourceSql = sourceData.sql;

                var migrator = await SqlMigrator<int>.InitializeAsync(dest);
                var intoTable = DbObject.Parse(step.DestTable);
                var mappings = GetForeignKeyMapping(step);

                using (var txn = dest.BeginTransaction())
                {
                    try
                    {
                        await migrator.CopyRowsAsync(dest, sourceData.table, step.DestIdentityColumn, intoTable.Schema, intoTable.Name, mappings, txn: txn, maxRows: maxRows);
                        success = true;
                        message = "Step succeeded.";
                        insertSql = migrator.MigrateCommand.GetInsertStatement();
                    }
                    catch (Exception exc)
                    {
                        message = exc.Message;                        
                    }
                    finally
                    {
                        txn.Rollback();
                    }                    
                }                
            });

            return (success, message, sourceSql, insertSql);
        }

        private class ValidationMessage
        {
            /// <summary>
            /// DataMigration.Column.Key or "_step"
            /// </summary>
            public string Context { get; set; }
            public string Message { get; set; }
        }
    }
}

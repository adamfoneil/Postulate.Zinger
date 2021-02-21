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
                var sourceCols = await getSchemaColumns(source, step.SourceFromWhere);                               
                var destCols = await getSchemaColumns(dest, $"SELECT * FROM {step.DestTable}");

                step.SourceIdentityColumn = findIdentityColumn(sourceCols);
                step.DestIdentityColumn = findIdentityColumn(destCols);

                // add matching columns
                var columns = (from src in nonIdentityColumns(sourceCols)
                               join dst in nonIdentityColumns(destCols) on columnName(src) equals columnName(dst)
                               select new DataMigration.Column()
                               {
                                   Source = columnName(src),
                                   Dest = columnName(dst)
                               }).ToList();

                var notInDest = nonIdentityColumns(sourceCols).Select(row => columnName(row)).Except(nonIdentityColumns(destCols).Select(row => columnName(row)));
                columns.AddRange(notInDest.Select(col => new DataMigration.Column()
                {
                    Source = col
                }));

                var notInSrc = nonIdentityColumns(destCols).Select(row => columnName(row)).Except(nonIdentityColumns(sourceCols).Select(row => columnName(row)));
                columns.AddRange(notInSrc.Select(col => new DataMigration.Column()
                {
                    Dest = col
                }));

                result.AddRange(columns);
            });

            step.Columns = result.ToArray();

            string columnName(DataRow row) => row.Field<string>("ColumnName");            

            IEnumerable<DataRow> nonIdentityColumns(DataTable schemaTable) => schemaTable.AsEnumerable().Where(row => !IsIdentity(row));

            async Task<DataTable> getSchemaColumns(SqlConnection connection, string sql)
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

            string findIdentityColumn(DataTable schemaTable)
            {
                try
                {
                    return columnName(schemaTable.AsEnumerable().First(row => IsIdentity(row)));
                }
                catch 
                {
                    return null;
                }                
            }
        }

        private bool IsIdentity(DataRow row) => row.Field<bool>("IsIdentity");

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
                    await migrator.CopyRowsAsync(dest, sourceTable, step.SourceIdentityColumn, dbobj.Schema, dbobj.Name, fkmapping);                    
                }
            });
        }

        private Dictionary<string, string> GetForeignKeyMapping(DataMigration.Step step) =>
            step.Columns
                .Where(col => !string.IsNullOrEmpty(col.KeyMapTable))
                .ToDictionary(col => col.Source, col => col.KeyMapTable);
        
        private async Task<DataTable> QuerySourceTableAsync(SqlConnection cnSource, DataMigration.Step step, object parameters = null)
        {
            var columns = string.Join(", ", step.Columns
                .Select(col => (col.Source.StartsWith("=")) ? 
                    col.Source.Substring(1) : 
                    $"[{col.Source}]"));

            var query = $"SELECT {columns} FROM {step.SourceFromWhere}";

            return await cnSource.QueryTableAsync(query, parameters);
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
    }
}

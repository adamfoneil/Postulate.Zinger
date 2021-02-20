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
        /// and let me know what columns didn't match so I know what I have to fill in manually.
        /// Todo: let me know incompatible columns (they have the same name, but mismatched types)
        /// </summary>
        public async Task<DataMigration.Step> CreateStepAsync(
            string sourceConnection, string destConnection, string fromWhere, string destTable, object parameters = null)
        {
            DataMigration.Step step = new DataMigration.Step()
            {
                SourceFromWhere = fromWhere,
                DestTable = destTable
            };            

            await ExecuteWithConnectionsAsync(sourceConnection, destConnection, async (source, dest) =>
            {
                var sourceSql = $"SELECT * FROM {fromWhere}";
                var sourceCols = await source.QuerySchemaTableAsync(sourceSql, parameters);

                var destSql = $"SELECT * FROM {destTable}";
                var destCols = await dest.QuerySchemaTableAsync(destSql);

                // add matching columns
                var columns = (from src in sourceCols.AsEnumerable()
                              join dst in destCols.AsEnumerable() on columnName(src) equals columnName(dst)
                              select new DataMigration.Column()
                              {
                                  Source = columnName(src),
                                  Dest = columnName(dst)
                              }).ToList();

                var notInDest = sourceCols.AsEnumerable().Select(row => columnName(row)).Except(destCols.AsEnumerable().Select(row => columnName(row)));
                columns.AddRange(notInDest.Select(col => new DataMigration.Column()
                {
                    Source = col
                }));
                
                var notInSrc = destCols.AsEnumerable().Select(row => columnName(row)).Except(sourceCols.AsEnumerable().Select(row => columnName(row)));
                columns.AddRange(notInSrc.Select(col => new DataMigration.Column()
                {
                    Dest = col
                }));

                step.Columns = columns.ToArray();    
            });

            return step;

            string columnName(DataRow row) => row.Field<string>("ColumnName");

            IEnumerable<DataRow> nonIdentityColumns(DataTable schemaTable) => 
                schemaTable.AsEnumerable().Where(row => !row.Field<bool>("IsIdentityColumn"));
         }

        private async Task ExecuteWithConnectionsAsync(string sourceConnection, string destConnection, Func<SqlConnection, SqlConnection, Task> execute)
        {
            var allConnections = _savedConnections.Connections.ToDictionary(item => item.Name, item => item.ConnectionString);

            using (var cnSource = new SqlConnection(allConnections[sourceConnection]))
            {
                using (var cnDest = new SqlConnection(allConnections[destConnection]))
                {
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
        
        private async Task<DataTable> QuerySourceTableAsync(SqlConnection cnSource, DataMigration.Step step, object parameters)
        {
            var columns = string.Join(", ", step.Columns
                .Select(col => (col.Source.StartsWith("=")) ? 
                    col.Source : 
                    $"[{col.Source}]"));

            var query = $"SELECT {columns} FROM {step.SourceFromWhere}";

            return await cnSource.QueryTableAsync(query, parameters);
        }

        public class ColumnInfo
        {
            public string Name { get; set; }
            public bool Required { get; set; }
        }
    }
}

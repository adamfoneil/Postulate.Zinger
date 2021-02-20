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

            await ExecuteWithConnectionsAsync(sourceConnection, destConnection, async (source, dest) =>
            {
                var sourceSql = $"SELECT * FROM {fromWhere}";
                var sourceCols = await source.QuerySchemaTableAsync(sourceSql, parameters);

                var destSql = $"SELECT * FROM {destTable}";
                var destCols = await dest.QuerySchemaTableAsync(destSql);

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

                step.Columns = columns.ToArray();

                var srcIdCol = sourceCols.AsEnumerable().FirstOrDefault(row => isIdentity(row));
                if (srcIdCol != null) step.SourceIdentityColumn = columnName(srcIdCol);

                var destIdCol = destCols.AsEnumerable().FirstOrDefault(row => isIdentity(row));
                if (destIdCol != null) step.DestIdentityColumn = columnName(destIdCol);
            });            

            return step;

            string columnName(DataRow row) => row.Field<string>("ColumnName");

            bool isIdentity(DataRow row) => row.Field<bool>("IsIdentity");            

            IEnumerable<DataRow> nonIdentityColumns(DataTable schemaTable) =>
                schemaTable.AsEnumerable().Where(row => isIdentity(row));
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

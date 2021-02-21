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

            step.Columns = result.ToArray();            

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

        /// <summary>
        /// gets error messages associated with a step (i.e. missing required columns, incompatible types, under-sized dest columns)
        /// </summary>
        public async Task<ILookup<string, string>> ValidateStepAsync(DataMigration.Step step, DataMigration migration)
        {
            List<ValidationMessage> results = new List<ValidationMessage>();

            await ExecuteWithConnectionsAsync(migration.SourceConnection, migration.DestConnection, async (source, dest) =>
            {
                var schemaCols = await GetStepSchemaColumns(source, dest, step, migration.GetParameters());

                AddUnrecognizedColumns(step.Columns, col => col.Source, schemaCols.sourceColumns);               
                AddUnrecognizedColumns(step.Columns, col => col.Dest, schemaCols.destColumns);

                // missing required dest columns
                var required = schemaCols.destColumns.AsEnumerable().Where(row => IsRequired(row)).Select(row => ColumnName(row)).ToHashSet();
                var missing = step.Columns.Where(col => col.SourceIsEmpty && required.Contains(col.Dest)).Select(col => new ValidationMessage()
                {
                    Context = col.Key,
                    Message = $"{col.Dest} is required, but not provided."
                });

                results.AddRange(missing);

                // incompatible types

                // under-sized dest columns
            });

            return results.ToLookup(row => row.Context, row => row.Message);

            void AddUnrecognizedColumns(IEnumerable<DataMigration.Column> columns, Func<DataMigration.Column, string> columnSelector, DataTable schemaColumns)
            {                
                var schemaColumnNames = schemaColumns.AsEnumerable().Select(row => ColumnName(row)).ToHashSet();
                var unrecognized = columns.Where(col => !string.IsNullOrEmpty(columnSelector.Invoke(col)) && !schemaColumnNames.Contains(columnSelector.Invoke(col))).Select(col => new { column = col, name = columnSelector.Invoke(col) });

                results.AddRange(unrecognized.Select(col => new ValidationMessage()
                {
                    Context = col.column.Key,
                    Message = $"Column name {col.name} is not recognized."
                }));
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
    }
}

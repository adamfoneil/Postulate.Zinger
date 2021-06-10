using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Zinger.Services.ScriptGen
{
    /// <summary>
    /// looks for tables with FK values that route to different tenant Ids
    /// </summary>
    public class VerifyTenantIsolation : ScriptGenBase
    {
        public override string Title => "Verify Tenant Isolation";

        public override string Sql => 
            @"WITH [source] AS (
                SELECT 
                    [t].[object_id] AS [ObjectId],
                    SCHEMA_NAME([t].[schema_id]) AS [Schema],
                    [name] AS [TableName],
                    (SELECT COUNT(1) FROM [sys].[foreign_keys] WHERE [parent_object_id]=[t].[object_id]) AS [FKCount]
                FROM 
                    [sys].[tables] [t]
            ) SELECT *
            FROM [source]
            WHERE [FKCount]>=2";

        protected override IEnumerable<string> RequiredParameters() => new string[]
        {
            "tenantTableSchema",
            "tenantTableName"
        };
        
        protected override async Task<string> GetScriptCommandAsync(IDbConnection connection, IEnumerable<Parameter> parameters, DataRow dataRow)
        {
            var fkInfo = await GetFKColumnsForTable(connection, dataRow.Field<int>("ObjectId"));

            throw new NotImplementedException();
        }

        private async Task<IEnumerable<FKInfoResult>> GetFKColumnsForTable(IDbConnection connection, int tableId)
        {
            return await connection.QueryAsync<FKInfoResult>(
                @"SELECT 
                    [fk].[name] AS [ConstraintName], 
                    [fk].[object_id] AS [ObjectId],     
                    SCHEMA_NAME([ref_t].[schema_id]) AS [ReferencedSchema],
                    [ref_t].[object_id] AS [ReferencedObjectId],
                    [ref_t].[name] AS [ReferencedTable],
                    [ref_col].[name] AS [ReferencedColumn],
                    SCHEMA_NAME([child_t].[schema_id]) AS [ReferencingSchema],
                    [child_t].[name] AS [ReferencingTable],
                    [child_col].[name] AS [ReferencingColumn]    
                FROM 
                    [sys].[foreign_keys] [fk]    
                    INNER JOIN [sys].[foreign_key_columns] [fkcol] ON [fk].[object_id]=[fkcol].[constraint_object_id]
                    INNER JOIN [sys].[tables] [child_t] ON [fkcol].[parent_object_id]=[child_t].[object_id]
                    INNER JOIN [sys].[columns] [child_col] ON
                        [child_t].[object_id]=[child_col].[object_id] AND
                        [fkcol].[parent_column_id]=[child_col].[column_id]
                    INNER JOIN [sys].[tables] [ref_t] ON [fkcol].[referenced_object_id]=[ref_t].[object_id]
                    INNER JOIN [sys].[columns] [ref_col] ON
                        [ref_t].[object_id]=[ref_col].[object_id] AND
                        [fkcol].[referenced_column_id]=[ref_col].[column_id]
                WHERE
                    [fk].[parent_object_id]=@tableId", new { tableId });
        }
    }

    public class FKInfoResult
    {
        public string ConstraintName { get; set; }
        public int ObjectId { get; set; }
        public int ReferencedObjectId { get; set; }
        public string ReferencedSchema { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
        public string ReferencingSchema { get; set; }
        public string ReferencingTable { get; set; }
        public string ReferencingColumn { get; set; }
    }

}

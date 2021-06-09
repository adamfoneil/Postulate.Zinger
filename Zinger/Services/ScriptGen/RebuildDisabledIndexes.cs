using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Zinger.Services.ScriptGen
{
    public class RebuildDisabledIndexes : ScriptGenBase
    {
        public override string Title => "Enable Disabled Indexes";

        public override string Sql => 
            @"SELECT 
	            [t].[name] AS [TableName], 
	            SCHEMA_NAME([t].[schema_id]) AS [Schema]	
            FROM 
	            [sys].[indexes] [ndx]
	            INNER JOIN [sys].[tables] [t] ON [ndx].[object_id]=[t].[object_id]
            WHERE 
	            [ndx].[is_disabled]=1
            GROUP BY
	            [t].[name], 
	            SCHEMA_NAME([t].[schema_id])";

        protected override async Task<string> GetScriptCommandAsync(IDbConnection connection, IEnumerable<Parameter> parameters, DataRow dataRow)
        {
            var result = $"ALTER INDEX ALL ON [{dataRow.Field<string>("Schema")}].[{dataRow.Field<string>("TableName")}] REBUILD;";
            return await Task.FromResult(result);
        }
    }
}

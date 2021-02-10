using System;
using System.Data;

namespace Zinger.Services.ScriptGen
{
    public class FindColumnLengths : ScriptGenBase
    {
        public override string Title => "Find Column Lengths";

        public override string Sql => 
            @"SELECT
	            SCHEMA_NAME([tbl].[schema_id]) AS [Schema],
	            [tbl].[name] AS [Table],
	            [col].[name] AS [Column]
            FROM
	            [sys].[columns] [col]
	            INNER JOIN [sys].[tables] [tbl] ON [col].[object_id]=[tbl].[object_id]
            WHERE
	            TYPE_NAME([col].[system_type_id]) LIKE '%varchar'";

        protected override string GetScriptCommand(DataRow dataRow)
        {
            return $"SELECT '{dataRow["Schema"]}' AS [Schema], '{dataRow["Table"]}' AS [Table], '{dataRow["Column"]}' AS [Column], MAX(LEN([{dataRow["Column"]}])) AS [Length] FROM [{dataRow["Schema"]}].[{dataRow["Table"]}]";
        }
    }
}

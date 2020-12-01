using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Data;

namespace Zinger.Services
{
    /// <summary>
    /// interprets a block of C# text to generate a database table
    /// </summary>
    public abstract class SqlTableBuilder
    {
        public IEnumerable<TableObject> ParseCSharp(string input)
        {
            List<TableObject> results = new List<TableObject>();
            
            var tree = CSharpSyntaxTree.ParseText(input);
            


            return results;
        }


        public class TableObject
        {
            public string Name { get; set; }
            public IEnumerable<ColumnObject> Columns { get; set; }
            public IEnumerable<IndexObject> Indexes { get; set; }
        }

        public class ColumnObject
        {
            public string Name { get; set; }
            public DbType DataType { get; set; }
            public bool IsNullable { get; set; }
            public int StringLength { get; set; }            
        }

        public class IndexObject
        {
            public string Name { get; set; }            
        }
    }
}

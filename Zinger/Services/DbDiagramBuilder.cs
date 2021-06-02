using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zinger.Services
{
    public class DbDiagramBuilder
    {
        /// <summary>
        /// gets syntax compatible with dbdiagram.io for a set of objects
        /// </summary>
        public StringBuilder GetSyntax(IEnumerable<Table> tables, IEnumerable<ForeignKey> foreignKeys)
        {
            var result = new StringBuilder();
            
            var foreignKeysByTable = foreignKeys.ToLookup(fk => fk.ReferencingTable);
            
            foreach (var tbl in tables)
            {
                var columns = ColumnSyntax(tbl, foreignKeysByTable[tbl], tables);

                result.AppendLine($"table {tbl.Name} {{");
                foreach (var col in columns)
                {
                    result.AppendLine($"  {col}");
                }
                result.AppendLine("}");
                result.AppendLine();
            }

            return result;
        }

        private IEnumerable<string> ColumnSyntax(Table table, IEnumerable<ForeignKey> foreignKeys, IEnumerable<Table> tables)
        {            
            var foreignKeysByColumn = foreignKeys                
                .ToDictionary(item => item.Columns.First().ReferencingName);

            var indexes = table.Indexes.ToLookup(ndx => ndx.Type);
            var pkColumns = (indexes.Contains(IndexType.PrimaryKey)) ?
                indexes[IndexType.PrimaryKey].SelectMany(ndx => ndx.Columns).Select(col => col.Name) :
                Enumerable.Empty<string>();
            
            foreach (var col in table.Columns)
            {
                string result = $"{col.Name} {col.DataType}";

                if (pkColumns.Contains(col.Name)) result += " pk";

                if (foreignKeysByColumn.ContainsKey(col.Name))
                {
                    var fk = foreignKeysByColumn[col.Name];
                    var comment = (tables.Contains(fk.ReferencedTable)) ? string.Empty : "//";
                    result += comment + $" [ref: > {fk.ReferencedTable.Name}.{fk.Columns.First().ReferencedName}]";
                }

                yield return result;
            }
        }
    }
}

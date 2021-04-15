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
                var columns = ColumnSyntax(tbl, foreignKeysByTable[tbl]);

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

        private IEnumerable<string> ColumnSyntax(Table table, IEnumerable<ForeignKey> foreignKeys)
        {            
            var foreignKeysByColumn = foreignKeys
                .ToDictionary(item => item.Columns.First().ReferencingName);

            foreach (var col in table.Columns)
            {
                string result = $"{col.Name} {col.DataType}";
                if (foreignKeysByColumn.ContainsKey(col.Name))
                {
                    result += $" [ref: > {foreignKeysByColumn[col.Name].ReferencedTable.Name}.{foreignKeysByColumn[col.Name].Columns.First().ReferencedName}]";
                }
                yield return result;
            }
        }
    }
}

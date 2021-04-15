using SqlSchema.Library.Models;
using System;
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

        private IEnumerable<string> ColumnSyntax(Table tbl, IEnumerable<ForeignKey> foreignKeys)
        {
            throw new NotImplementedException();
            var foreignKeysByColumn = foreignKeys
                .ToDictionary(item => string.Join("|", item.Columns.Select(col => col.ReferencingName)));
        }
    }
}

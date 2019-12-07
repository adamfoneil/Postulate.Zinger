using SqlSchema.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class ColumnNode : TreeNode
    {
        public ColumnNode(Column column, IEnumerable<ForeignKey> foreignKeys) : base(GetNodeText(column, foreignKeys))
        {
            ImageKey =
                (column.InPrimaryKey) ? "primaryKey" :
                (IsForeignKey(column, foreignKeys, out _, out _)) ? "shortcut" :
                "column";
            SelectedImageKey = ImageKey;                
        }

        protected static bool IsForeignKey(Column column, IEnumerable<ForeignKey> foreignKeys, out Table referencedTable, out string referencedColumn)
        {
            var flattenedFKs = foreignKeys.SelectMany(fk => fk.Columns, (fk, col) => new { ForeignKey = fk, Column = col });
            var referencedFK = flattenedFKs.FirstOrDefault(fk => fk.Column.ReferencingName.Equals(column.Name));

            referencedTable = referencedFK?.ForeignKey.ReferencedTable;
            referencedColumn = referencedFK?.Column.ReferencedName;

            return (referencedFK != null);
        }

        public static string GetNodeText(Column column, IEnumerable<ForeignKey> foreignKeys)
        {
            string result = column.Name;

            if (IsForeignKey(column, foreignKeys, out Table referencedTable, out string referencedColumn))
            {
                result += $" -> {referencedTable.Schema}.{referencedTable.Name}.{referencedColumn}";
            }

            return result;
        }
    }
}

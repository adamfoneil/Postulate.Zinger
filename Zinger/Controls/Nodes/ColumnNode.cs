using SqlSchema.Library.Models;
using SqlSchema.SqlServer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class ColumnNode : TreeNode
    {
        public ColumnNode(Column column) : base(GetNodeText(column))
        {
            ImageKey = "column";
            SelectedImageKey = "column";
        }

        public ColumnNode(ForeignKeyColumn column) : base($"{column.ReferencingName} = {column.ReferencedName}")
        {
            ImageKey = "shortcut";
            SelectedImageKey = "shortcut";
        }

        public ColumnNode(Column column, IEnumerable<ForeignKey> foreignKeys, string identityColumn) : base(GetNodeText(column))
        {
            ImageKey =
                (column.InPrimaryKey) ? "primaryKey" :
                (IsForeignKey(column, foreignKeys, out _, out _)) ? "shortcut" :                
                (column.Name.Equals(identityColumn)) ? "unique" :
                "column";

            SelectedImageKey = ImageKey;

            if (IsForeignKey(column, foreignKeys, out Table referencedTable, out string referencedColumn))
            {
                Nodes.Add(new TableNode(referencedTable, referencedColumn));                
            }
        }

        protected static bool IsForeignKey(Column column, IEnumerable<ForeignKey> foreignKeys, out Table referencedTable, out string referencedColumn)
        {
            var flattenedFKs = foreignKeys.SelectMany(fk => fk.Columns, (fk, col) => new { ForeignKey = fk, Column = col });
            var referencedFK = flattenedFKs.FirstOrDefault(fk => fk.Column.ReferencingName.Equals(column.Name));

            referencedTable = referencedFK?.ForeignKey.ReferencedTable;
            referencedColumn = referencedFK?.Column.ReferencedName;

            return (referencedFK != null);
        }

        public static string GetNodeText(Column column)
        {
            string result = column.Name;

            result += $": {column.DisplayDataType()}, {nullable()}";            

            return result;

            string nullable() => (column.IsNullable) ? "null" : "not null";
        }
    }
}

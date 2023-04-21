using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class TableNode : ColumnContainerNode
    {
        private readonly bool _queryEnabled = false;

        public TableNode(ForeignKey foreignKey) : base($"{foreignKey.ReferencingTable.Schema}.{foreignKey.ReferencingTable.Name}")
        {
            ImageKey = "table";
            SelectedImageKey = "table";
            Columns = foreignKey.Columns.Select(col => new ColumnNode(col)).ToList();
            RowCount = foreignKey.ReferencingTable.RowCount;
            _queryEnabled = false;
        }

        public TableNode(Table parentTable, string parentColumn) : base($"{parentTable.Schema}.{parentTable.Name}.{parentColumn}", false)
        {
            ImageKey = "unique";
            SelectedImageKey = "unique";
            _queryEnabled = false;
        }

        public TableNode(Table table) : base(table.Name + DisplayAttributeStr(table))
        {
            Table = table;
            ImageKey = "table";
            SelectedImageKey = "table";
            Columns = new List<ColumnNode>();
            RowCount = table.RowCount;

            var attr = DisplayAttributes(table);
            if (attr.Any()) ToolTipText = string.Join(", ", attr.Select(a => a.Text));

            _queryEnabled = true;
        }

        private static string DisplayAttributeStr(Table table)
        {
            var attr = DisplayAttributes(table);
            if (attr.Any()) return $" [ {string.Join(", ", attr.Select(item => item.Code))} ]";

            return string.Empty;
        }

        private static IEnumerable<(string Code, string Text)> DisplayAttributes(Table table)
        {
            if (table.HasChangeTracking)
            {
                yield return ("ct", "Change Tracking");
            }
        }

        public long RowCount { get; }

        public Table Table { get; }

        public override bool SqlQueryEnabled => _queryEnabled;
        public override string SqlQuery => $"SELECT * FROM [{Table.Schema}].[{Table.Name}]";
        public override string ModelClassName => Table.Name;
        public override DbObject DbObject => Table;        
    }
}

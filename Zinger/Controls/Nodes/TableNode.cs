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

        public TableNode(Table table) : base(table.Name)
        {
            Table = table;
            ImageKey = "table";
            SelectedImageKey = "table";
            Columns = new List<ColumnNode>();
            RowCount = table.RowCount;
            _queryEnabled = true;
        }

        public long RowCount { get; }

        public Table Table { get; }

        public override bool SqlQueryEnabled => _queryEnabled;
        public override string SqlQuery => $"SELECT * FROM [{Table.Schema}].[{Table.Name}]";
        public override string ModelClassName => Table.Name;

        private string _alias;
        public string Alias 
        { 
            get => _alias; 
            set 
            {
                _alias = value;
                Text = $"{Table.Name} - {_alias}"; 
            }
        }
    }
}

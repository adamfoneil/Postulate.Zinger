using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class TableNode : ColumnContainerNode
    {        
        public TableNode(ForeignKey foreignKey) : base($"{foreignKey.ReferencingTable.Schema}.{foreignKey.ReferencingTable.Name}")
        {
            ImageKey = "table";
            SelectedImageKey = "table";            
            Columns = foreignKey.Columns.Select(col => new ColumnNode(col)).ToList();
            RowCount = foreignKey.ReferencingTable.RowCount;
        }

        public TableNode(Table parentTable, string parentColumn) : base($"{parentTable.Schema}.{parentTable.Name}.{parentColumn}", false)
        {
            ImageKey = "unique";
            SelectedImageKey = "unique";
        }

        public TableNode(Table table) : base(table.Name)
        {
            ImageKey = "table";
            SelectedImageKey = "table";            
            Columns = new List<ColumnNode>();
            RowCount = table.RowCount;
        }
               
        public long RowCount { get; }
        
    }
}

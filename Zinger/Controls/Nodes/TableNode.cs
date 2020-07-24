using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class TableNode : TreeNode
    {        
        public TableNode(ForeignKey foreignKey) : base($"{foreignKey.ReferencingTable.Schema}.{foreignKey.ReferencingTable.Name}")
        {
            ImageKey = "table";
            SelectedImageKey = "table";
            Nodes.Add(new PlaceholderNode());
            Columns = foreignKey.Columns.Select(col => new ColumnNode(col)).ToList();
        }

        public TableNode(Table parentTable, string parentColumn) : base($"{parentTable.Schema}.{parentTable.Name}.{parentColumn}")
        {
            ImageKey = "unique";
            SelectedImageKey = "unique";
        }

        public TableNode(Table table) : base(table.Name)
        {
            ImageKey = "table";
            SelectedImageKey = "table";
            Nodes.Add(new PlaceholderNode());
            Columns = new List<ColumnNode>();
            RowCount = table.RowCount;
        }
        
        public List<ColumnNode> Columns { get; internal set; }

        public long RowCount { get; }
        
        public bool HasPlaceholder
        {
            get { return this.Nodes[0] is PlaceholderNode; }
        }

        public void LoadColumns()
        {
            List<TreeNode> remove = new List<TreeNode>();
            remove.AddRange(Nodes.OfType<PlaceholderNode>());
            foreach (var node in remove) Nodes.Remove(node);

            // add columns in reverse order so we can insert them ahead of any child nodes, maintaining original order
            for (int i = Columns.Count - 1; i >= 0; i--) Nodes.Insert(0, Columns[i]);           
        }
    }
}

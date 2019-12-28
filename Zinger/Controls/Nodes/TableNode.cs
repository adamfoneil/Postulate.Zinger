using SqlSchema.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class TableNode : TreeNode
    {
        public TableNode(string name) : base(name)
        {
            ImageKey = "table";
            SelectedImageKey = "table";
            Nodes.Add(new PlaceholderNode());
            Columns = new List<ColumnNode>();
        }
        
        public List<ColumnNode> Columns { get; internal set; }
        
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

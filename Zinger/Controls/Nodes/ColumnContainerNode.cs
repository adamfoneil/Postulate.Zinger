using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public abstract class ColumnContainerNode : TreeNode
    {
        public ColumnContainerNode(string text, bool addPlaceholder = true) : base(text)
        {
            if (addPlaceholder) Nodes.Add(new PlaceholderNode());
        }

        public List<ColumnNode> Columns { get; internal set; }

        public bool HasPlaceholder => this.Nodes[0] is PlaceholderNode;

        public void LoadColumns()
        {
            List<TreeNode> remove = new List<TreeNode>();
            remove.AddRange(Nodes.OfType<PlaceholderNode>());
            foreach (var node in remove) Nodes.Remove(node);

            // add columns in reverse order so we can insert them ahead of any child nodes, maintaining original order
            for (int i = Columns.Count - 1; i >= 0; i--) Nodes.Insert(0, Columns[i]);
        }

        public abstract bool SqlQueryEnabled { get; }
        public abstract string SqlQuery { get; }
        public abstract string ModelClassName { get; }        
        public abstract DbObject DbObject { get; }
    }
}

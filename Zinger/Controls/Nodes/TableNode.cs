using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class TableNode : TreeNode
    {
        public TableNode(string name) : base(name)
        {
            ImageKey = "table";
            SelectedImageKey = "table";
        }
    }
}

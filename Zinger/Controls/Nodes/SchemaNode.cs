using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zinger.Controls.Nodes
{
    public class SchemaNode : TreeNode
    {
        public SchemaNode(string name) : base(name)
        {
            ImageKey = "schema";
            SelectedImageKey = "schema";
        }
    }
}

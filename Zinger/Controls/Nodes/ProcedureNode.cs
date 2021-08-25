using SqlSchema.Library.Models;
using System.Linq;
using System.Windows.Forms;
using Zinger.Interfaces;

namespace Zinger.Controls.Nodes
{
    public class ProcedureNode : TreeNode, IDbObject
    {
        public ProcedureNode(Procedure procedure) : base(procedure.Name)
        {
            ImageKey = "proc";
            SelectedImageKey = "proc";
            Procedure = procedure;

            var folder = new FolderNode("Parameters", procedure.Arguments.Count());
            Nodes.Add(folder);
            folder.Nodes.AddRange(procedure.Arguments.Select(p => new ParameterNode(p)).ToArray());
        }

        public Procedure Procedure { get; }

        public DbObject DbObject => Procedure;
    }
}

using SqlSchema.Library.Models;
using System.Windows.Forms;
using Zinger.Interfaces;
using View = SqlSchema.Library.Models.View;

namespace Zinger.Controls.Nodes
{
    public class SynonymNode : TreeNode, IDbObject
    {
        public SynonymNode(Synonym synonym) : base(synonym.Name)
        {
            ImageKey = "shortcut";
            SelectedImageKey = "shortcut";
            Synonym = synonym;

            if (synonym.ReferencedObject is Procedure proc)
            {
                Nodes.Add(new ProcedureNode(proc));
            }

            if (synonym.ReferencedObject is View view)
            {
                Nodes.Add(new ViewNode(view));
            }
        }

        public Synonym Synonym { get;}

        public DbObject DbObject => Synonym;
    }
}

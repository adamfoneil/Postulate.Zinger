using SqlSchema.Library.Models;
using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class ViewNode : ColumnContainerNode
    {
        public ViewNode(View view) : base(view.Name)
        {
            ImageKey = "view";
            SelectedImageKey = "view";
            Columns = view.Columns.Select(col => new ColumnNode(col)).ToList();
            View = view;
        }

        public View View { get; }

        public override string SqlQuery => $"SELECT * FROM [{View.Schema}].[{View.Name}]";
        public override bool SqlQueryEnabled => true;
        public override string ModelClassName => View.Name;
        public override DbObject DbObject => View;        
    }
}

using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class ViewNode : ColumnContainerNode
    {
        public ViewNode(SqlSchema.Library.Models.View view) : base(view.Name)
        {
            ImageKey = "view";
            SelectedImageKey = "view";
            Columns = view.Columns.Select(col => new ColumnNode(col)).ToList();
            View = view;
        }

        public SqlSchema.Library.Models.View View { get; }

        public override string SqlQuery => $"SELECT * FROM [{View.Schema}].[{View.Name}]";
        public override bool SqlQueryEnabled => true;
        public override string ModelClassName => View.Name;
    }
}

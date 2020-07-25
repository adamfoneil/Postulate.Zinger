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
        }
    }
}

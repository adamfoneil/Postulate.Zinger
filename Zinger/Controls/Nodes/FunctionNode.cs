using SqlSchema.Library.Models;
using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class FunctionNode : ColumnContainerNode
    {
        public FunctionNode(TableFunction function) : base(function.Name)
        {
            ImageKey = "table-function";
            SelectedImageKey = "table-function";
            Columns = function.Columns.Select(col => new ColumnNode(col)).ToList();

            var folder = new FolderNode("Parameters", function.Arguments.Count());
            Nodes.Add(folder);
            folder.Nodes.AddRange(function.Arguments.Select(p => new ParameterNode(p)).ToArray());            
        }        
    }
}

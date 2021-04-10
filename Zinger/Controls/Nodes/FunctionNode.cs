using SqlSchema.Library.Models;
using System.Linq;

namespace Zinger.Controls.Nodes
{
    public class FunctionNode : ColumnContainerNode
    {
        public FunctionNode(TableFunction function) : base(function.Name)
        {
            TableFunction = function;

            ImageKey = "table-function";
            SelectedImageKey = "table-function";
            Columns = function.Columns.Select(col => new ColumnNode(col)).ToList();

            var folder = new FolderNode("Parameters", function.Arguments.Count());
            Nodes.Add(folder);
            folder.Nodes.AddRange(function.Arguments.Select(p => new ParameterNode(p)).ToArray());
        }

        public TableFunction TableFunction { get; }

        public override bool SqlQueryEnabled => true;
        public override string SqlQuery => $"SELECT * FROM [{TableFunction.Schema}].[{TableFunction.Name}]()";
        public override string ModelClassName => TableFunction.Name;
        public override DbObject DbObject => TableFunction;
        public override bool HasViewableDefinition => true;
    }
}

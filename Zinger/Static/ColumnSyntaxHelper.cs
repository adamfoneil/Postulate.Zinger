using SqlSchema.Library.Models;

namespace Zinger.Static
{
    internal static class ColumnSyntaxHelper
    {
        internal static (string Type, string Nullable) Tokens(this Column column)
        {
            string typeSyntax = column.DataType;

            if (column.DataType.StartsWith("nvar") || column.DataType.StartsWith("var"))
            {
                var max = (column.MaxLength == -1) ? "max" : column.MaxLength.ToString();
                typeSyntax += $"({max})";
            }

            return (typeSyntax, column.IsNullable ? "NULL" : "NOT NULL");
        }

        internal static string TypeSyntax(this Column column) => column.Tokens().Type;

        internal static string NullableSyntax(this Column column) => column.Tokens().Nullable;
    }
}

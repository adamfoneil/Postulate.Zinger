using Renci.SshNet.Messages;
using SqlSchema.Library.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Zinger.Static;

namespace Zinger.Services
{
    public static class SyntaxBuilder
    {
        public static void GenerateAndCopy(Table table, Func<Column, int, string> columnTemplate, string message, bool padBetweenNamesAndTypes = false, bool lineEndCommas = true, Func<Table, string, string> outerTemplate = null)
        {
            try
            {
                var result = Generate(table, columnTemplate, padBetweenNamesAndTypes, lineEndCommas, outerTemplate);
                Clipboard.SetText(result);
                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public static void GenerateAndCopyTableVariable(Table table, string message, bool padBetweenNamesAndTypes = false, bool lineEndCommas = true)
        {
            try
            {
                var result = GenerateTableVariable(table, padBetweenNamesAndTypes, lineEndCommas);
                Clipboard.SetText(result);
                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        public static string GenerateTableVariable(Table table, bool padBetweenNamesAndTypes = false, bool lineEndCommas = true) =>
            Generate(table, (col, padding) => $"[{col.Name}] {new string(' ', padding)}{col.TypeSyntax()} {col.NullableSyntax()}", padBetweenNamesAndTypes, lineEndCommas, (table2, cols) =>
$@"DECLARE @{table2.Name} TABLE (
{cols}
)");
        
        public static string Generate(Table table, Func<Column, int, string> columnTemplate, bool padBetweenNamesAndTypes = false, bool lineEndCommas = true, Func<Table, string, string> outerTemplate = null)
        {
            string output = null;

                var maxColLength = table.Columns.Max(col => col.Name.Length);
                var separator = lineEndCommas ? ",\r\n\t" : "\r\n\t,";
                output = string.Join(separator, table.Columns.Select(col =>
                {
                    int padding = (padBetweenNamesAndTypes) ? maxColLength - col.Name.Length : 0;
                    return columnTemplate.Invoke(col, padding);
                }));

                if (outerTemplate != null)
                {
                    output = outerTemplate.Invoke(table, output);
                }                

            return output;
        }
    }
}

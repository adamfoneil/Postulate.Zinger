using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zinger.Services
{
    /// <summary>
    /// appears in menu as a script generator that can be invoked.
    /// Used to generate scripts from queries, such as enabling all disabled indexes
    /// </summary>
    public abstract class ScriptGenBase
    {
        public abstract string Title { get; }

        public abstract string Sql { get; }

        protected abstract string GetScriptCommand(DataRow dataRow);

        public string Generate(QueryProvider queryProvider)
        {
            StringBuilder output = new StringBuilder();
            
            var queryResult = queryProvider.Execute(Sql, nameof(ScriptGenBase), Enumerable.Empty<Parameter>());
            
            foreach (DataRow row in queryResult.DataTable.Rows)
            {
                output.AppendLine(GetScriptCommand(row));
                output.AppendLine();
            }

            return output.ToString();
        }

        public void GenerateAndCopy(QueryProvider queryProvider)
        {
            try
            {
                var result = Generate(queryProvider);
                Clipboard.SetText(result);
                MessageBox.Show($"{Title} script copied to clipboard.");
            }
            catch
            {
                Clipboard.SetText(Sql);
                MessageBox.Show("There was an error generating the script. The source SQL was copied instead.");
            }
        }
    }
}

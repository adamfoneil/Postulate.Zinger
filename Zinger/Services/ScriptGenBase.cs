using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected abstract Task<string> GetScriptCommandAsync(IDbConnection connection, DataRow dataRow);

        public async Task<string> GenerateAsync(QueryProvider queryProvider)
        {
            StringBuilder output = new StringBuilder();
            
            using (var cn = queryProvider.GetConnection())
            {
                var queryResult = queryProvider.Execute(Sql, nameof(ScriptGenBase), Enumerable.Empty<Parameter>());

                foreach (DataRow row in queryResult.DataTable.Rows)
                {
                    output.AppendLine(await GetScriptCommandAsync(cn, row));
                    output.AppendLine();
                }
            }            

            return output.ToString();
        }

        public async Task GenerateAndCopyAsync(QueryProvider queryProvider)
        {
            try
            {
                var result = await GenerateAsync(queryProvider);
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

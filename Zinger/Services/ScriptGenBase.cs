using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zinger.Exceptions;

namespace Zinger.Services
{
    public delegate IEnumerable<Parameter> GetParametersHandler();

    /// <summary>
    /// appears in menu as a script generator that can be invoked.
    /// Used to generate scripts from queries, such as enabling all disabled indexes
    /// </summary>
    public abstract class ScriptGenBase
    {
        public abstract string Title { get; }

        public abstract string Sql { get; }

        protected virtual IEnumerable<string> RequiredParameters() => Enumerable.Empty<string>();

        protected abstract Task<string> GetScriptCommandAsync(IDbConnection connection, IEnumerable<Parameter> parameters, DataRow dataRow);

        public event GetParametersHandler OnGetParameters;

        private IEnumerable<Parameter> GetParameters()
        {
            var result = OnGetParameters?.Invoke();
            return result ?? Enumerable.Empty<Parameter>();
        }

        public async Task<string> GenerateAsync(QueryProvider queryProvider)
        {
            var parameters = GetParameters();
            var missingParams = RequiredParameters().Except(parameters.Select(p => p.Name));
            if (missingParams.Any()) throw new ParametersMissingException(missingParams);

            StringBuilder output = new StringBuilder();
            
            using (var cn = queryProvider.GetConnection())
            {
                var queryResult = queryProvider.Execute(Sql, nameof(ScriptGenBase), Enumerable.Empty<Parameter>());

                foreach (DataRow row in queryResult.DataTable.Rows)
                {
                    output.AppendLine(await GetScriptCommandAsync(cn, parameters, row));
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
            catch (ParametersMissingException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch
            {
                Clipboard.SetText(Sql);
                MessageBox.Show("There was an error generating the script. The source SQL was copied instead.");
            }
        }
    }
}

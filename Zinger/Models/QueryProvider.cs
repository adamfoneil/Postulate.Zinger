using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Zinger.Models
{
    public abstract class QueryProvider        
    {
        protected readonly string _connectionString;

        public QueryProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected abstract IDbConnection GetConnection();

        protected abstract IDbCommand GetCommand(string query, IDbConnection connection);

        protected abstract IDbDataAdapter GetAdapter(IDbCommand command);

        public long Milleseconds { get; private set; }

        public string ResolvedQuery { get; private set; }

        public BindingList<Parameter> Parameters { get; set; }

        public DataTable Execute(string query)
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                Parameter[] parameters;
                ResolvedQuery = ResolveParameters(query, out parameters);
                using (var cmd = GetCommand(ResolvedQuery, cn))
                {
                    foreach (var p in parameters)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = p.Name;
                        param.DbType = p.DataType;
                        param.Value = p.Value;
                        cmd.Parameters.Add(param);
                    }

                    var adapter = GetAdapter(cmd);
                    try
                    {
                        DataSet dataSet = new DataSet();
                        Stopwatch sw = Stopwatch.StartNew();
                        adapter.Fill(dataSet);
                        sw.Stop();
                        Milleseconds = sw.ElapsedMilliseconds;
                        return dataSet.Tables[0];
                    }
                    catch (Exception exc)
                    {
                        throw new Exception($"Error running query: {exc.Message}", exc);
                    }
                }
            }
        }

        private string ResolveParameters(string query, out Parameter[] parameters)
        {
            parameters = Parameters.Where(p => p.Value != null).ToArray();

            string result = query;

            string whereClause = string.Join(" AND ", parameters.Select(p => p.Expression));
            result = result.Replace("{where}", whereClause);
            result = result.Replace("{andWhere}", " AND " + whereClause);

            return result;
        }

        public class Parameter
        {
            public string Name { get; set; }
            public DbType DataType { get; set; }

            /// <summary>
            /// Indicates that generated query property is nullable (and Expression is therefore optional)
            /// </summary>
            public bool IsOptional { get; set; }

            /// <summary>
            /// Text to insert in the query's WHERE clause
            /// </summary>
            public string Expression { get; set; }

            public object Value { get; set; }
        }
    }
}
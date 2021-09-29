using Dapper.QX;
using Dapper.QX.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Zinger.Services
{
    public enum ProviderType
    {
        MySql,
        SqlServer,
        OleDb,
        SqlCe
    }

    public abstract class QueryProvider
    {
        protected readonly string _connectionString;
        protected readonly QueryClassBuilder _classBuilder;

        public QueryProvider(string connectionString)
        {
            _connectionString = connectionString;
            _classBuilder = new QueryClassBuilder(GetCommand);
        }

        public abstract IDbConnection GetConnection();

        public abstract IDbCommand GetCommand(string query, IDbConnection connection);

        protected abstract IDbDataAdapter GetAdapter(IDbCommand command);

        public abstract ProviderType ProviderType { get; }

        public (bool result, string message) TestConnection()
        {
            try
            {
                using (var cn = GetConnection())
                {
                    cn.Open();
                    return (true, null);
                }
            }
            catch (Exception exc)
            {
                return (false, exc.Message);
            }
        }

        public long Milleseconds { get; private set; }

        public string ResolvedQuery { get; private set; }

        public bool BeautifyColumnNames { get; set; }

        public DataTable GetSchemaTable(string query, IEnumerable<Parameter> parameters = null)
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                using (var cmd = GetCommand(query, cn))
                {
                    cmd.CommandTimeout = 120;
                    Parameter.AddToQuery(parameters?.Where(p => !p.IsArray()), cmd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.GetSchemaTable();
                    }
                }
            }
        }

        public ExecuteResult Execute(string query, string queryName, IEnumerable<Parameter> parameters)
        {
            var result = new ExecuteResult();

            using (var cn = GetConnection())
            {
                cn.Open();

                ResolvedQuery = ResolveQuery(query, parameters);
                using (var cmd = GetCommand(ResolvedQuery, cn))
                {
                    Parameter.AddToQuery(parameters.Where(p => !p.IsArray()), cmd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var schemaTable = reader.GetSchemaTable();
                        result.ResultClass = _classBuilder.GetResultClass(schemaTable, queryName, BeautifyColumnNames);
                    }

                    result.QueryClass = _classBuilder.GetQueryClass(cn, query, queryName, parameters, true);

                    var adapter = GetAdapter(cmd);
                    try
                    {
                        DataSet dataSet = new DataSet();
                        Stopwatch sw = Stopwatch.StartNew();
                        adapter.Fill(dataSet);
                        sw.Stop();
                        Milleseconds = sw.ElapsedMilliseconds;
                        result.DataTable = dataSet.Tables[0];
                    }
                    catch (Exception exc)
                    {
                        throw new Exception($"Error running query: {exc.Message}", exc);
                    }
                }
            }

            return result;
        }

        private string ResolveQuery(string query, IEnumerable<Parameter> parameters)
        {
            var expressionParams = parameters?.Where(p => p.Value != null && p.Expression != null).ToArray() ?? Enumerable.Empty<Parameter>().ToArray();

            string result = QueryHelper.ResolveInjectedCriteria(query, expressionParams.Select(p => p.Expression));

            var arrayParams = parameters?.Where(p => p.IsArray()) ?? Enumerable.Empty<Parameter>();
            foreach (var arrayParam in arrayParams)
            {
                result = result.Replace($"@{arrayParam.Name}", "(" + arrayParam.ArrayValueString() + ")");
            }

            return RegexHelper.RemovePlaceholders(result);
        }

        public class ExecuteResult
        {
            public DataTable DataTable { get; set; }
            public string ResultClass { get; set; }
            public string QueryClass { get; set; }
        }
    }
}
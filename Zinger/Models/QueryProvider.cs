﻿using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Zinger.Models
{
    public abstract class QueryProvider        
    {
        protected readonly string _connectionString;

        public QueryProvider(string connectionString)
        {
            _connectionString = connectionString;
            Parameters = new BindingList<Parameter>();
        }

        protected abstract IDbConnection GetConnection();

        protected abstract IDbCommand GetCommand(string query, IDbConnection connection);

        protected abstract IDbDataAdapter GetAdapter(IDbCommand command);        

        public long Milleseconds { get; private set; }

        public string ResolvedQuery { get; private set; }
        
        public BindingList<Parameter> Parameters { get; set; }

        public ExecuteResult Execute(string query, string queryName)
        {
            var result = new ExecuteResult();

            using (var cn = GetConnection())
            {
                cn.Open();
                
                ResolvedQuery = BuildWhereClause(query);
                using (var cmd = GetCommand(ResolvedQuery, cn))
                {
                    foreach (var p in Parameters)
                    {
                        var param = cmd.CreateParameter();
                        param.ParameterName = p.Name;
                        param.DbType = p.DataType;
                        param.Value = p.Value ?? DBNull.Value;
                        cmd.Parameters.Add(param);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        var schemaTable = reader.GetSchemaTable();
                        result.ResultClass = GetCSharpClass(schemaTable, queryName);
                    }

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

        private static string GetCSharpClass(DataTable schemaTable, string queryName)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine($"public class {queryName}Result\r\n{{");

            var columnInfo = CSharpPropertiesFromSchemaTable(schemaTable);

            foreach (var column in columnInfo)
            {
                output.AppendLine($"\tpublic {column.CSharpType} {column.PropertyName} {{ get; set; }}");
            }

            output.AppendLine("}"); // end class

            return output.ToString();
        }

        private string BuildWhereClause(string query)
        {
            var expressionParams = Parameters?.Where(p => p.Value != null && p.Expression != null).ToArray() ?? Enumerable.Empty<Parameter>().ToArray();         
            
            string result = query;

            string whereClause = string.Join(" AND ", expressionParams.Select(p => p.Expression));
            result = result.Replace("{where}", whereClause);
            result = result.Replace("{andWhere}", (!string.IsNullOrEmpty(whereClause)) ? " AND " + whereClause : string.Empty);

            return result;
        }

        private static IEnumerable<ColumnInfo> CSharpPropertiesFromSchemaTable(DataTable schemaTable)
        {
            // find duplicate column names so we can append an incremental digit to the end of the name
            var dupColumns = schemaTable.AsEnumerable()
                .GroupBy(row => row.Field<string>("ColumnName"))
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key)
                .ToDictionary(name => name, name => 0);

            List<ColumnInfo> results = new List<ColumnInfo>();

            using (CSharpCodeProvider provider = new CSharpCodeProvider())
            {
                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = row.Field<string>("ColumnName");
                    if (dupColumns.ContainsKey(columnName)) dupColumns[columnName]++;
                    ColumnInfo columnInfo = new ColumnInfo()
                    {
                        Name = columnName,
                        CSharpType = CSharpTypeName(provider, row.Field<Type>("DataType")),
                        IsNullable = row.Field<bool>("AllowDBNull"),
                        Index = (dupColumns.ContainsKey(columnName)) ? dupColumns[columnName] : 0
                    };

                    if (columnInfo.IsNullable && !columnInfo.CSharpType.ToLower().Equals("string")) columnInfo.CSharpType += "?";
                    if (columnInfo.CSharpType.ToLower().Equals("string") && row.Field<int>("ColumnSize") < int.MaxValue) columnInfo.Size = row.Field<int>("ColumnSize");

                    results.Add(columnInfo);
                }
            }

            return results;
        }

        private static string CSharpTypeName(CSharpCodeProvider provider, Type type)
        {
            CodeTypeReference typeRef = new CodeTypeReference(type);
            return provider.GetTypeOutput(typeRef).Replace("System.", string.Empty);
        }

        public class ColumnInfo
        {
            public string Name { get; set; }
            public string CSharpType { get; set; }
            public int? Size { get; set; }
            public bool IsNullable { get; set; }
            public int Index { get; set; }

            public string PropertyName
            {
                get { return (Index > 0) ? $"{Name}{Index}" : Name; }
            }
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

        public class ExecuteResult
        {
            public DataTable DataTable { get; set; }
            public string ResultClass { get; set; }
        }
    }
}
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Zinger.Models;

namespace Zinger.Services
{
    public class QueryClassBuilder
    {
        private readonly Func<string, IDbConnection, IDbCommand> _getCommand;

        public QueryClassBuilder(Func<string, IDbConnection, IDbCommand> getCommand)
        {
            _getCommand = getCommand;
        }

        public static string ResultClassFirstLine(string queryName)
        {
            return $"public class {queryName}Result";
        }

        public static string QueryClassFirstLine(string queryName, bool testable)
        {
            return $"public class {queryName} : Query<{queryName}Result>";
        }

        public string GetQueryClass(IDbConnection connection, string query, string queryName, IEnumerable<Parameter> parameters, bool testable)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(QueryClassFirstLine(queryName, testable) + "\r\n{");
            output.AppendLine($"\tpublic {queryName}() : base(");
            output.AppendLine($"\t\t@\"{Indent(2, query)}\")\r\n\t{{\r\n\t}}");

            var paramDictionary = parameters.Where(p => !string.IsNullOrEmpty(p.Name)).ToDictionary(row => row.ToColumnName());
            if (parameters?.Any() ?? false)
            {
                output.AppendLine();
                IEnumerable<ColumnInfo> properties = CSharpPropertiesFromParameters(connection, parameters);
                foreach (var p in properties)
                {
                    if (!string.IsNullOrEmpty(paramDictionary[p.Name].Expression))
                    {
                        output.AppendLine($"\t[Where(\"{paramDictionary[p.Name].Expression}\")]");
                    }
                    output.AppendLine($"\tpublic {p.CSharpType} {p.PascalCaseName} {{ get; set; }}\r\n");
                }
            }

            output.AppendLine("}"); // end class

            return output.ToString();

        }

        public string GetResultClass(DataTable schemaTable, string queryName, bool beautifyColumnNames)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(ResultClassFirstLine(queryName) + "\r\n{");

            var columnInfo = CSharpPropertiesFromSchemaTable(schemaTable);

            foreach (var column in columnInfo)
            {
                string prettyName = column.Name;

                if (beautifyColumnNames && IsUglyColumnName(column.PropertyName, out prettyName))
                {
                    output.AppendLine($"\t[Column(\"{column.PropertyName}\")]");
                }
                output.AppendLine($"\tpublic {column.CSharpType} {prettyName} {{ get; set; }}");
            }

            output.AppendLine("}"); // end class

            return output.ToString();
        }

        /// <summary>
        /// Indents lines of the given input except for the first line
        /// </summary>
        private static string Indent(int tabCount, string input)
        {
            const string separator = "\r\n";
            string[] lines = input
                .Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                .Select((line, i) => (i > 0) ? new string('\t', tabCount) + line : line).ToArray();
            return string.Join(separator, lines);
        }

        private static bool IsUglyColumnName(string propertyName, out string prettyName)
        {
            prettyName = propertyName;

            if (propertyName.Contains("_") || propertyName.ToUpper().Equals(propertyName))
            {
                string[] parts = propertyName.Split('_');
                prettyName = string.Join("", parts.Select(s => TitleCase(s)));
                return true;
            }

            return false;
        }

        private static string TitleCase(string input)
        {
            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
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

        private IEnumerable<ColumnInfo> CSharpPropertiesFromParameters(IDbConnection connection, IEnumerable<Parameter> parameters)
        {
            var includeParams = parameters.Where(p => !string.IsNullOrEmpty(p.Name) && !p.IsArray());
            if (!includeParams.Any()) return Enumerable.Empty<ColumnInfo>();

            string columns = string.Join(", ", includeParams.Select(p => $"{p.ToParamName()} AS {p.ToColumnName()}"));
            string dummyQuery = $"SELECT {columns}";

            using (var cmd = _getCommand.Invoke(dummyQuery, connection))
            {
                Parameter.AddToQuery(includeParams, cmd);                
                using (var reader = cmd.ExecuteReader())
                {
                    var schemaTable = reader.GetSchemaTable();
                    return CSharpPropertiesFromSchemaTable(schemaTable);
                }
            }
        }

        public class ColumnInfo
        {
            public string Name { get; set; }
            public string CSharpType { get; set; }
            public int? Size { get; set; }
            public bool IsNullable { get; set; }
            public int Index { get; set; }

            public string PascalCaseName
            {
                get
                {
                    string name = PropertyName;
                    return name.Substring(0, 1).ToUpper() + name.Substring(1);
                }
            }

            public string PropertyName
            {
                get { return (Index > 0) ? $"{Name}{Index}" : Name; }
            }

            public string ToColumnName()
            {
                return (Name.StartsWith("@")) ? Name.Substring(1) : Name;
            }
        }
    }
}

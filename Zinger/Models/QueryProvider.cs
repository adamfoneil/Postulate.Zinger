using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
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
		}

		protected abstract IDbConnection GetConnection();

		protected abstract IDbCommand GetCommand(string query, IDbConnection connection);

		protected abstract IDbDataAdapter GetAdapter(IDbCommand command);

		public long Milleseconds { get; private set; }

		public string ResolvedQuery { get; private set; }

		public bool BeautifyColumnNames { get; set; }

		public ExecuteResult Execute(string query, string queryName, IEnumerable<Parameter> parameters)
		{
			var result = new ExecuteResult();

			using (var cn = GetConnection())
			{
				cn.Open();

				ResolvedQuery = ResolveQuery(query, parameters);
				using (var cmd = GetCommand(ResolvedQuery, cn))
				{
					AddParameters(parameters.Where(p => !p.IsArray()), cmd);

					using (var reader = cmd.ExecuteReader())
					{
						var schemaTable = reader.GetSchemaTable();
						result.ResultClass = GetCSharpResultClass(schemaTable, queryName, BeautifyColumnNames);
					}

					result.QueryClass = GetCSharpQueryClass(cn, query, queryName, parameters);

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

		public static string ResultClassFirstLine(string queryName)
		{
			return $"public class {queryName}Result";
		}

		public static string QueryClassFirstLine(string queryName)
		{
			return $"public class {queryName} : Query<{queryName}Result>";
		}

		private string GetCSharpQueryClass(IDbConnection connection, string query, string queryName, IEnumerable<Parameter> parameters)
		{
			StringBuilder output = new StringBuilder();

			output.AppendLine(QueryClassFirstLine(queryName) + "\r\n{");
			output.AppendLine($"\tpublic {queryName}() : base(");
			output.AppendLine($"\t\t@\"{Indent(2, query)}\")\r\n\t{{\r\n\t}}");

			var paramDictionary = parameters.ToDictionary(row => row.ToColumnName());
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
					output.AppendLine($"\tpublic {p.CSharpType} {p.PascalCaseName} {{ get; set }}\r\n");
				}
			}

			output.AppendLine("}"); // end class

			return output.ToString();
		}

		private IEnumerable<ColumnInfo> CSharpPropertiesFromParameters(IDbConnection connection, IEnumerable<Parameter> parameters)
		{
			string columns = string.Join(", ", parameters.Select(p => $"{p.ToParamName()} AS {p.ToColumnName()}"));
			string dummyQuery = $"SELECT {columns}";

			using (var cmd = GetCommand(dummyQuery, connection))
			{
				AddParameters(parameters, cmd);
				using (var reader = cmd.ExecuteReader())
				{
					var schemaTable = reader.GetSchemaTable();
					return CSharpPropertiesFromSchemaTable(schemaTable);
				}
			}
		}

		private static void AddParameters(IEnumerable<Parameter> parameters, IDbCommand cmd)
		{
			foreach (var p in parameters)
			{
				var param = cmd.CreateParameter();
				param.ParameterName = p.Name;
				param.DbType = p.DataType;
				param.Value = p.Value ?? DBNull.Value;
				cmd.Parameters.Add(param);
			}
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

		private static string GetCSharpResultClass(DataTable schemaTable, string queryName, bool beautifyColumnNames)
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

		private string ResolveQuery(string query, IEnumerable<Parameter> parameters)
		{
			var expressionParams = parameters?.Where(p => p.Value != null && p.Expression != null).ToArray() ?? Enumerable.Empty<Parameter>().ToArray();

			string result = query;

			string whereClause = string.Join(" AND ", expressionParams.Select(p => p.Expression));
			result = result.Replace("{where}", whereClause);
			result = result.Replace("{andWhere}", (!string.IsNullOrEmpty(whereClause)) ? " AND " + whereClause : string.Empty);

			var arrayParams = parameters?.Where(p => p.IsArray()) ?? Enumerable.Empty<Parameter>();
			foreach (var arrayParam in arrayParams)
			{
				result = result.Replace($"@{arrayParam.Name}", "(" + arrayParam.ArrayValueString() + ")");
			}

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

			/// <summary>
			/// Inserts an @ sign ahead of name if not present to assure it can be used as param literal
			/// </summary>
			/// <returns></returns>
			public string ToParamName()
			{
				return (!Name.StartsWith("@")) ? "@" + Name : Name;
			}

			public string ToColumnName()
			{
				return (Name.StartsWith("@")) ? Name.Substring(1) : Name;
			}

			public bool IsArray()
			{
				string valueString = Value?.ToString();
				if (!string.IsNullOrEmpty(valueString))
				{
					return (valueString.StartsWith("[") && valueString.EndsWith("]"));
				}

				return false;
			}

			public object GetValue()
			{
				return (IsArray()) ? ValueToArray() : Value;
			}

			public string ArrayValueString()
			{
				string valueString = Value.ToString();
				return valueString.Substring(1, valueString.Length - 2);
			}

			private string[] ValueToArray()
			{
				string arrayString = ArrayValueString();
				return arrayString.Split(',', ';').Select(s => s.Trim()).ToArray();
			}
		}

		public class ExecuteResult
		{
			public DataTable DataTable { get; set; }
			public string ResultClass { get; set; }
			public string QueryClass { get; set; }
		}
	}
}
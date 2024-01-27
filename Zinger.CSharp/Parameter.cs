using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Zinger.Services
{
    public class Parameter
    {
        public string Name { get; set; }
        public DbType DataType { get; set; }

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

        public static void AddToQuery(IEnumerable<Parameter> parameters, IDbCommand cmd, Func<object, DbType, object> convertParamValue = null)
        {
            if (parameters == null) return;

            foreach (var p in parameters)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = p.Name;
                param.DbType = p.DataType;
                param.Value = ((convertParamValue != null) ? convertParamValue.Invoke(p.Value, p.DataType) : p.Value) ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }
        }
    }
}

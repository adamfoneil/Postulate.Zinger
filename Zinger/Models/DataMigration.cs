using System;
using System.Collections.Generic;
using System.Linq;

namespace Zinger.Models
{
    public class DataMigration
    {
        public string SourceConnection { get; set; }
        public string DestConnection { get; set; }

        public Step[] Steps { get; set; }

        public Parameter[] Parameters { get; set; }

        public class Step
        {
            public int Order { get; set; }

            /// <summary>
            /// query that provides rows to migrate
            /// </summary>
            public string SourceFromWhere { get; set; }

            /// <summary>
            /// identity column in SourceSql results, provides the SourceId in migration tracking
            /// </summary>
            public string SourceIdentityColumn { get; set; }

            /// <summary>
            /// rows are inserted into this table,
            /// use with Column.KeyMapTable
            /// </summary>
            public string DestTable { get; set; }

            /// <summary>
            /// DestTable's identity column, provides the NewId in migration tracking
            /// </summary>
            public string DestIdentityColumn { get; set; }
            
            public Column[] Columns { get; set; }
        }

        public class Column
        {
            public string Source { get; set; }
            public string Dest { get; set; }
            public string KeyMapTable { get; set; }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public Dictionary<string, string> GetParameters() => Parameters?.ToDictionary(row => row.Name, row => row.Value);        
    }
}

namespace Zinger.Models
{
    public class DataMigration
    {
        public string SourceConnection { get; set; }
        public string DestConnection { get; set; }

        public class Step
        {
            public int Order { get; set; }
            public string FromSql { get; set; }
            public string IntoTable { get; set; } // use with Column.KeyMapTable
            public string IdentityColumn { get; set; }
            public Column[] Columns { get; set; }
        }

        public class Column
        {
            public string Source { get; set; }
            public string Dest { get; set; }
            public string KeyMapTable { get; set; }
        }
    }


}

using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace Zinger.Services.Providers
{
    public class SqliteQueryProvider : QueryProvider
    {
        public SqliteQueryProvider(string connectionString) : base(connectionString)
        {
        }

        public override ProviderType ProviderType => ProviderType.Sqlite;

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new SqliteCommand(query, connection as SqliteConnection);

        public override IDbConnection GetConnection() => new SqliteConnection();
        
        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            // https://github.com/adamfoneil/Postulate.Zinger/issues/43
            throw new NotImplementedException();
        }
    }
}

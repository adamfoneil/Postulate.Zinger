using Npgsql;
using System.Data;

namespace Zinger.Services.Providers
{
    internal class PostgreSqlProvider : QueryProvider
    {
        public PostgreSqlProvider(string connectionString) : base(connectionString)
        {                
        }

        public override ProviderType ProviderType => ProviderType.PostgreSql;

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new NpgsqlCommand(query, connection as NpgsqlConnection);

        public override IDbConnection GetConnection() => new NpgsqlConnection(_connectionString);

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new NpgsqlDataAdapter(command as NpgsqlCommand);        
    }
}

using MySql.Data.MySqlClient;
using System.Data;

namespace Zinger.Models
{
    public class MySqlQueryProvider : QueryProvider
    {
        public override ProviderType ProviderType => ProviderType.MySql;

        public MySqlQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new MySqlDataAdapter(command as MySqlCommand);
        }

        public override IDbCommand GetCommand(string query, IDbConnection connection)
        {
            return new MySqlCommand(query, connection as MySqlConnection);
        }

        public override IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
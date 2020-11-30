using System.Data;
using System.Data.OleDb;

namespace Zinger.Services.Providers
{
    public class OleDbQueryProvider : QueryProvider
    {
        public override ProviderType ProviderType => ProviderType.OleDb;

        public OleDbQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new OleDbDataAdapter(command as OleDbCommand);

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new OleDbCommand(query, connection as OleDbConnection);

        public override IDbConnection GetConnection() => new OleDbConnection(_connectionString);
    }
}
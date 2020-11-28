using System.Data;
using System.Data.OleDb;
using Zinger.Services;

namespace Zinger.Services.Providers
{
    public class OleDbQueryProvider : QueryProvider
    {
        public override ProviderType ProviderType => ProviderType.OleDb;

        public OleDbQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new OleDbDataAdapter(command as OleDbCommand);
        }

        public override IDbCommand GetCommand(string query, IDbConnection connection)
        {
            return new OleDbCommand(query, connection as OleDbConnection);
        }

        public override IDbConnection GetConnection()
        {
            return new OleDbConnection(_connectionString);
        }
    }
}
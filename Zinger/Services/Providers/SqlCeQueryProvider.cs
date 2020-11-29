using System.Data;
using System.Data.SqlServerCe;

namespace Zinger.Services.Providers
{
    public class SqlCeQueryProvider : QueryProvider
    {
        public SqlCeQueryProvider(string connectionString) : base(connectionString)
        {
        }

        public override ProviderType ProviderType => ProviderType.SqlCe;

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new SqlCeCommand(query, connection as SqlCeConnection);

        public override IDbConnection GetConnection() => new SqlCeConnection(_connectionString);

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new SqlCeDataAdapter(command as SqlCeCommand);
    }
}

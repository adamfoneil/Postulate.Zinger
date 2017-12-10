using System.Data;
using System.Data.SqlClient;

namespace Zinger.Models
{
    public class SqlServerQueryProvider : QueryProvider
    {
        public SqlServerQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new SqlDataAdapter(command as SqlCommand);
        }

        protected override IDbCommand GetCommand(string query, IDbConnection connection)
        {
            return new SqlCommand(query, connection as SqlConnection);
        }

        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
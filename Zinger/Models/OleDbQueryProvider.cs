using System.Data;
using System.Data.OleDb;

namespace Zinger.Models
{
	public class OleDbQueryProvider : QueryProvider
	{
		public OleDbQueryProvider(string connectionString) : base(connectionString)
		{
		}

		protected override IDbDataAdapter GetAdapter(IDbCommand command)
		{
			return new OleDbDataAdapter(command as OleDbCommand);
		}

		protected override IDbCommand GetCommand(string query, IDbConnection connection)
		{
			return new OleDbCommand(query, connection as OleDbConnection);
		}

		public override IDbConnection GetConnection()
		{
			return new OleDbConnection(_connectionString);
		}
	}
}
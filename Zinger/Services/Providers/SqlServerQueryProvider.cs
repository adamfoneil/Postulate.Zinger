﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace Zinger.Services.Providers
{
    public class SqlServerQueryProvider : QueryProvider
    {
        public override ProviderType ProviderType => ProviderType.SqlServer;

        public SqlServerQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new SqlDataAdapter(command as SqlCommand);

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new SqlCommand(query, connection as SqlConnection);

        public override IDbConnection GetConnection() => new SqlConnection(_connectionString);
    }
}
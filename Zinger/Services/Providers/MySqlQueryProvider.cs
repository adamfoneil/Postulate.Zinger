﻿using MySql.Data.MySqlClient;
using System.Data;

namespace Zinger.Services.Providers
{
    public class MySqlQueryProvider : QueryProvider
    {
        public override ProviderType ProviderType => ProviderType.MySql;

        public MySqlQueryProvider(string connectionString) : base(connectionString)
        {
        }

        protected override IDbDataAdapter GetAdapter(IDbCommand command) => new MySqlDataAdapter(command as MySqlCommand);

        public override IDbCommand GetCommand(string query, IDbConnection connection) => new MySqlCommand(query, connection as MySqlConnection);

        public override IDbConnection GetConnection() => new MySqlConnection(_connectionString);
    }
}
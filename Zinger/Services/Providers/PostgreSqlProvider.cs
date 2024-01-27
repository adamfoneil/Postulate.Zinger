using Npgsql;
using System;
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

        /// <summary>
        /// Unlike other ADO providers, Postgres doesn't do implicit type conversions on param
        /// values to their target DbTypes. It complains if you try to assign a string to an int,
        /// for instance, so I do this explicitly.
        /// </summary>
        public override object ConvertParamValue(object @object, DbType dbType)
        {
            if (dbType == DbType.Int32) return Convert.ToInt32(@object);

            return @object;
        }
        
    }
}

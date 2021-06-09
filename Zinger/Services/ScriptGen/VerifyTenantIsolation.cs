using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Zinger.Services.ScriptGen
{
    public class VerifyTenantIsolation : ScriptGenBase
    {
        public override string Title => "Verify Tenant Isolation";

        public override string Sql => "SELECT * FROM [sys].[foreign_keys]";

        protected override IEnumerable<string> RequiredParameters() => new string[]
        {
            "tenantTableSchema",
            "tenantTableName"
        };
        
        protected override Task<string> GetScriptCommandAsync(IDbConnection connection, IEnumerable<Parameter> parameters, DataRow dataRow)
        {
            throw new NotImplementedException();
        }
    }
}

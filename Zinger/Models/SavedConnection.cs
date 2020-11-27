using JsonSettings;
using JsonSettings.Library;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Zinger.Models
{
    public enum ProviderType
    {
        MySql,
        SqlServer,
        OleDb,
        SqlCe
    }

    public class SavedConnections : SettingsBase
    {
        public List<SavedConnection> Connections { get; set; }

        protected override void Initialize()
        {
            if (Connections == null) Connections = new List<SavedConnection>();
        }

        public override string Filename => BuildPath(Environment.SpecialFolder.ApplicationData, "Zinger", "Connections.json");
    }

    public class SavedConnection
    {
        public string Name { get; set; }

        [JsonProtect(DataProtectionScope.CurrentUser)]
        public string ConnectionString { get; set; }

        public ProviderType ProviderType { get; set; }

        public async Task<TestResult> TestAsync()
        {
            TestResult result = new TestResult();

            try
            {
                switch (ProviderType)
                {
                    case ProviderType.MySql:
                        using (var cn = new MySqlConnection(ConnectionString))
                        {
                            await cn.OpenAsync();
                        }
                        break;

                    case ProviderType.SqlServer:
                        using (var cn = new SqlConnection(ConnectionString))
                        {
                            await cn.OpenAsync();
                        }
                        break;

                    case ProviderType.OleDb:
                        using (var cn = new OleDbConnection(ConnectionString))
                        {
                            await cn.OpenAsync();
                        }
                        break;
                }
                result.OpenedSuccessfully = true;
            }
            catch (Exception exc)
            {
                result.ErrorMessage = exc.Message;
            }

            return result;
        }

        public class TestResult
        {
            public bool OpenedSuccessfully { get; set; }
            public string ErrorMessage { get; set; }
        }

        public override string ToString()
        {
            return $"{Name} ({ProviderType})";
        }
    }
}
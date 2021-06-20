using JsonSettings;
using JsonSettings.Library;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Zinger.Services;

namespace Zinger.Models
{
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

        public override string ToString()
        {
            return $"{Name} ({ProviderType})";
        }
    }
}
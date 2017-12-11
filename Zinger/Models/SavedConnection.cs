using AdamOneilSoftware;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Zinger.Models
{
    public enum ProviderType
    {
        MySql,
        SqlServer
    }

    [XmlRoot(ElementName = "ArrayOfSavedConnection")]
    public class SavedConnections : List<SavedConnection>
    {
    }

    public class SavedConnection
    {
        public string Name { get; set; }

        [XmlIgnore]
        public string ConnectionString { get; set; }

        public ProviderType ProviderType { get; set; }

        public string EncryptedConnection
        {
            get { return ConnectionString.Encrypt(); }
            set { ConnectionString = value.Decrypt(); }
        }
    }
}
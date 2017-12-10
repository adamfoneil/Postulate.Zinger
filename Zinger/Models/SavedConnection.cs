using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Zinger.Models
{
    public enum ProviderType
    {
        MySql,
        SqlServer
    }

    [XmlRoot(ElementName = "ArrayOfSavedConnection")]
    public class SavedConnections : List<SavedConnection>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            while (reader.Read())
            {
                reader.Read();

                SavedConnection sc = new SavedConnection();
                sc.Name = reader.ReadContentAsString();
                reader.Read();
                reader.Read();

                sc.ProviderType = (ProviderType)Enum.Parse(typeof(ProviderType), reader.ReadContentAsString());
                reader.Read();
                reader.Read();

                sc.ConnectionString = reader.ReadContentAsString().Decrypt();
                reader.Read();
                reader.Read();
                
                Add(sc);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
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
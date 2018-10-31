using AdamOneilSoftware;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zinger.Models
{
	public enum ProviderType
	{
		MySql,
		SqlServer,
		OleDb
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
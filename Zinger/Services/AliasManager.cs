using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;
using Zinger.Static;

namespace Zinger.Services
{
    /// <summary>
    /// stores user-defined table name abbreviations (aliases) associated full table names
    /// </summary>
    public class AliasManager
    {
        private readonly string _path = null;

        private string _fileName;
        private Dictionary<string, string> _aliases;

        public AliasManager(string path)
        {
            _path = path;
        }

        private string _connectionName;
        public string ConnectionName
        {
            get { return _connectionName; }
            set
            {
                Save();

                _connectionName = value;
                _fileName = System.IO.Path.Combine(_path, $"{_connectionName}.aliases.json");
                _aliases = (System.IO.File.Exists(_fileName)) ?
                    JsonHelper.DeserializeFile<Dictionary<string, string>>(_fileName) :
                    new Dictionary<string, string>();
            }
        }

        public Dictionary<string, string> Aliases { get => _aliases; }

        public bool ContainsTable(Table table) => ContainsTable($"{table.Schema}.{table.Name}", out _);

        public bool ContainsTable(string tableName, out string alias)
        {
            var entry = _aliases.FirstOrDefault(kp => kp.Value.Equals(tableName));
            if (!default(KeyValuePair<string, string>).Equals(entry))
            {
                alias = entry.Key;
                return true;
            }

            alias = null;
            return false;
        }

        public void Save()
        {
            if (_fileName == null) return;
            if (_aliases == null) return;
            JsonHelper.SerializeFile(_fileName, _aliases);
        }

        ~AliasManager()
        {
            Save();
        }
    }
}

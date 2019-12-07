using SqlSchema.Library;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zinger.Controls.Nodes;
using Zinger.Models;

namespace Zinger.Controls
{
    public partial class SchemaBrowser : UserControl
    {
        public SchemaBrowser()
        {
            InitializeComponent();
        }                

        private Dictionary<ProviderType, Analyzer> Analyzers
        {
            get
            {
                return new Dictionary<ProviderType, Analyzer>()
                {
                    { ProviderType.SqlServer, new SqlServerAnalyzer() }
                };
            }
        }

        public async Task FillAsync(ProviderType providerType, IDbConnection connection)
        {
            tvwObjects.Nodes.Clear();

            if (!Analyzers.ContainsKey(providerType))
            {
                MessageBox.Show($"Provider type {providerType.ToString()} not supported by object browser.");
                return;
            }
            
            var objects = await Analyzers[providerType].GetDbObjectsAsync(connection);

            try
            {
                tvwObjects.BeginUpdate();
                var schemaGroups = objects.Where(obj => obj.IsSelectable).GroupBy(obj => obj.Schema).OrderBy(grp => grp.Key);
                foreach (var schemaGrp in schemaGroups)
                {
                    var schemaNode = new SchemaNode(schemaGrp.Key);
                    tvwObjects.Nodes.Add(schemaNode);

                    var tables = schemaGrp.OfType<Table>().OrderBy(obj => obj.Name);
                    foreach (var table in tables)
                    {
                        var tableNode = new TableNode(table.Name);
                        schemaNode.Nodes.Add(tableNode);

                        var foreignKeys = (table as Table).GetParentForeignKeys(objects);
                        foreach (var col in table.Columns)
                        {
                            var columnNode = new ColumnNode(col, foreignKeys);
                            tableNode.Nodes.Add(columnNode);
                        }
                    }
                }
            }
            finally
            {
                tvwObjects.EndUpdate();
            }            
        }
    }
}

﻿using SqlSchema.Library;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System;
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
        private IEnumerable<DbObject> _objects;

        public event EventHandler<string> OperationStarted;
        public event EventHandler OperationEnded;

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

        public async Task FillAsync(ProviderType providerType, Func<IDbConnection> getConnection)
        {            
            if (!Analyzers.ContainsKey(providerType))
            {
                MessageBox.Show($"Provider type {providerType.ToString()} not supported by object browser.");
                return;
            }

            using (var cn = getConnection.Invoke())
            {
                _objects = await Analyzers[providerType].GetDbObjectsAsync(cn);                
            }

            LoadObjects();
        }

        private void LoadObjects(DbObjectSearch search = null)
        {
            try
            {
                statusStrip1.Visible = true;
                OperationStarted?.Invoke(this, "Loading objects...");

                tvwObjects.BeginUpdate();
                tvwObjects.Nodes.Clear();

                var schemas = _objects
                    .Where(obj => obj.IsSelectable && (search?.IsIncluded(obj) ?? true))
                    .GroupBy(obj => obj.Schema).OrderBy(grp => grp.Key);

                foreach (var schemaGrp in schemas)
                {
                    var schemaNode = new SchemaNode(schemaGrp.Key);
                    tvwObjects.Nodes.Add(schemaNode);

                    var tables = schemaGrp.OfType<Table>().OrderBy(obj => obj.Name);
                    foreach (var table in tables)
                    {
                        var tableNode = new TableNode(table.Name);
                        schemaNode.Nodes.Add(tableNode);

                        var foreignKeys = table.GetParentForeignKeys(_objects);
                        tableNode.Columns.AddRange(table.Columns.Select(col => new ColumnNode(col, foreignKeys)));

                        var childFKs = table.GetChildForeignKeys(_objects);
                        if (childFKs.Any())
                        {
                            var childFolderNode = new TreeNode("Child Tables");
                            tableNode.Nodes.Add(childFolderNode);

                            foreach (var childFK in childFKs)
                            {
                                var fkNode = new TableNode($"{childFK.ReferencingTable.Schema}.{childFK.ReferencingTable.Name}");
                                childFolderNode.Nodes.Add(fkNode);
                            }
                        }
                    }

                    schemaNode.Expand();
                }
            }
            finally
            {
                tvwObjects.EndUpdate();
                statusStrip1.Visible = false;
                OperationEnded?.Invoke(this, new EventArgs());
            }
        }

        private void selectColumnsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var node = tvwObjects.SelectedNode;
            tvwObjects.CheckBoxes = !tvwObjects.CheckBoxes;
        }

        private void tvwObjects_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var search = DbObjectSearch.Parse(tbSearch.Text);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        
        private class DbObjectSearch
        {
            public string SchemaName { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }

            public bool IsIncluded(DbObject dbObject)
            {
                throw new NotImplementedException();
            }

            public static DbObjectSearch Parse(string text)
            {
                if (text.StartsWith("."))
                {
                    return new DbObjectSearch() { ColumnName = text.Substring(1).Trim() };
                }

                string[] parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                return
                    (parts.Length == 1) ? new DbObjectSearch() { TableName = parts[0] } :
                    (parts.Length == 2) ? new DbObjectSearch() { TableName = parts[0], ColumnName = parts[1] } :
                    (parts.Length > 2) ? new DbObjectSearch() { SchemaName = parts[0], TableName = parts[1], ColumnName = parts[2] } :
                    null;                
            }
        }

        private void tvwObjects_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TableNode tableNode = e.Node as TableNode;
            if (tableNode != null)
            {
                if (tableNode.HasPlaceholder) tableNode.LoadColumns();
            }
        }
    }
}

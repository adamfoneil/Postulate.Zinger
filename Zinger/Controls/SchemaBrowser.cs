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
                statusStrip1.Visible = true;
                tvwObjects.BeginUpdate();
                
                var schemas = objects
                    .Where(obj => obj.IsSelectable)
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

                        var foreignKeys = table.GetParentForeignKeys(objects);
                        foreach (var col in table.Columns)
                        {
                            var columnNode = new ColumnNode(col, foreignKeys);
                            tableNode.Nodes.Add(columnNode);
                        }
                        
                        var childFKs = table.GetChildForeignKeys(objects);
                        if (childFKs.Any())
                        {
                            var childFolderNode = new TreeNode("children");
                            tableNode.Nodes.Add(childFolderNode);

                            foreach (var childFK in childFKs)
                            {
                                var fkNode = new TableNode($"{childFK.ReferencingTable.Schema}.{childFK.ReferencingTable.Name}");
                                childFolderNode.Nodes.Add(fkNode);
                            }
                        }
                    }
                }
            }
            finally
            {
                tvwObjects.EndUpdate();
                statusStrip1.Visible = false;
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

        private void tbSearch_TextChanged(object sender, System.EventArgs e)
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

            public static DbObjectSearch Parse(string text)
            {
                string[] parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                return
                    (parts.Length == 1) ? new DbObjectSearch() { TableName = parts[0] } :
                    (parts.Length == 2) ? new DbObjectSearch() { TableName = parts[0], ColumnName = parts[1] } :
                    (parts.Length == 3) ? new DbObjectSearch() { SchemaName = parts[0], TableName = parts[1], ColumnName = parts[2] } :
                    null;                
            }
        }
    }
}

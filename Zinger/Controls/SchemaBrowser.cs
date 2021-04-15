﻿using SqlSchema.Library;
using SqlSchema.Library.Interfaces;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zinger.Controls.Nodes;
using Zinger.Forms;
using Zinger.Models;
using Zinger.Services;
using Zinger.Static;
using Table = SqlSchema.Library.Models.Table;

namespace Zinger.Controls
{
    public partial class SchemaBrowser : UserControl
    {
        private IEnumerable<DbObject> _objects;
        private readonly TextBoxDelayHandler _searchBox;
        private readonly AliasManager _aliasManager;
        private readonly DbDiagramBuilder _diagramBuilder;

        private ProviderType _providerType;
        private Func<IDbConnection> _getConnection;
        private JoinResolver _joinResolver;

        public event EventHandler<string> OperationStarted;
        public event EventHandler OperationEnded;
        public event EventHandler<ColumnContainerNode> ModelClassRequested;
        public event EventHandler SchemaInspected;

        private TableNode _selectedTable;
        private ColumnContainerNode _selectedObject;

        public SchemaBrowser()
        {
            InitializeComponent();

            _searchBox = new TextBoxDelayHandler(tbSearch, 300);
            _searchBox.DelayedTextChanged += tbSearch_TextChanged;

            _aliasManager = new AliasManager(new Options().Folder);
            _diagramBuilder = new DbDiagramBuilder();
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

        public new void Focus()
        {
            tbSearch.Focus();
        }

        public bool IsSchemaSupported { get; private set; }

        public async Task FillAsync(ProviderType providerType, Func<IDbConnection> getConnection, string connectionName)
        {
            try
            {
                if (Analyzers.ContainsKey(providerType))
                {
                    _providerType = providerType;
                    _getConnection = getConnection;
                    _aliasManager.ConnectionName = connectionName;
                    await RefreshAsync();
                }

                IsSchemaSupported = Analyzers.ContainsKey(providerType);
                SchemaInspected?.Invoke(this, new EventArgs());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async Task RefreshAsync()
        {
            using (var cn = _getConnection.Invoke())
            {
                _objects = await Analyzers[_providerType].GetDbObjectsAsync(cn);
                _joinResolver = new JoinResolver(_objects, _aliasManager);
            }

            LoadObjects();
        }

        private void LoadObjects(DbObjectSearch search = null)
        {
            try
            {
                pbLoading.Visible = true;
                OperationStarted?.Invoke(this, "Loading objects...");

                tvwObjects.BeginUpdate();
                tvwObjects.Nodes.Clear();

                var schemas = _objects
                    .Where(obj => obj.IsSelectable && (search?.IsIncluded(obj) ?? true))
                    .GroupBy(obj => obj.Schema).OrderBy(grp => grp.Key);

                FolderNode folderNode = null;

                foreach (var schemaGrp in schemas)
                {
                    var schemaNode = new SchemaNode(schemaGrp.Key, schemaGrp.Count());
                    tvwObjects.Nodes.Add(schemaNode);

                    var tables = schemaGrp.OfType<Table>().OrderBy(obj => obj.Name);
                    if (tables.Any())
                    {
                        folderNode = new FolderNode("Tables", tables.Count());
                        schemaNode.Nodes.Add(folderNode);

                        foreach (var table in tables)
                        {
                            var tableNode = new TableNode(table);
                            if (_aliasManager.ContainsTable(table.ToString(), out string alias)) tableNode.Alias = alias;
                            folderNode.Nodes.Add(tableNode);

                            var foreignKeys = table.GetParentForeignKeys(_objects);
                            tableNode.Columns.AddRange(table.Columns.Select(col =>
                            {
                                var node = new ColumnNode(col, foreignKeys, table.IdentityColumn);
                                if (IsUniqueMultiColumn(table, col)) node.NodeFont = new Font(tvwObjects.Font, FontStyle.Bold);
                                return node;
                            }));

                            var childFKs = table.GetChildForeignKeys(_objects);
                            if (childFKs.Any())
                            {
                                var childFolderNode = new TreeNode($"Child Tables ({childFKs.Count()})") { ImageKey = "join", SelectedImageKey = "join" };
                                tableNode.Nodes.Add(childFolderNode);

                                foreach (var childFK in childFKs)
                                {
                                    var fkNode = new TableNode(childFK);
                                    childFolderNode.Nodes.Add(fkNode);
                                }
                            }
                        }
                        folderNode.Expand();
                    }

                    var views = schemaGrp.OfType<SqlSchema.Library.Models.View>().OrderBy(obj => obj.Name);
                    if (views.Any())
                    {
                        folderNode = new FolderNode("Views", views.Count());
                        schemaNode.Nodes.Add(folderNode);

                        foreach (var view in views)
                        {
                            var viewNode = new ViewNode(view);
                            folderNode.Nodes.Add(viewNode);
                        }
                    }

                    var functions = schemaGrp.OfType<TableFunction>().OrderBy(obj => obj.Name);
                    if (functions.Any())
                    {
                        folderNode = new FolderNode("Functions", functions.Count());
                        schemaNode.Nodes.Add(folderNode);

                        foreach (var func in functions)
                        {
                            var functionNode = new FunctionNode(func);
                            folderNode.Nodes.Add(functionNode);
                        }
                    }

                    schemaNode.Expand();
                }
            }
            finally
            {
                tvwObjects.EndUpdate();
                pbLoading.Visible = false;
                OperationEnded?.Invoke(this, new EventArgs());
            }
        }

        private bool IsUniqueMultiColumn(Table table, Column col)
        {
            return table
                .Indexes
                .Where(ndx => ndx.Type != IndexType.PrimaryKey && ndx.Type != IndexType.NonUnique && ndx.Columns.Count() > 1)
                .SelectMany(ndx => ndx.Columns)
                .Select(ndxCol => ndxCol.Name)
                .Contains(col.Name);
        }

        private void selectColumnsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var node = tvwObjects.SelectedNode;
            tvwObjects.CheckBoxes = !tvwObjects.CheckBoxes;
        }

        private void tvwObjects_MouseDown(object sender, MouseEventArgs e)
        {
            var hitTest = tvwObjects.HitTest(e.X, e.Y);
            _selectedObject = hitTest.Node as ColumnContainerNode;
            viewDefinitionToolStripMenuItem.Enabled = _selectedObject?.DbObject is IDefinition;

            findTable(hitTest.Node);

            void findTable(TreeNode node)
            {
                if (node == null) return;
                _selectedTable = node as TableNode;
                if (_selectedTable == null && node.Parent != null) findTable(node.Parent);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var search = DbObjectSearch.Parse(tbSearch.Text);
                LoadObjects(search);
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
                var criteria = new Dictionary<Func<string>, Func<string, bool>>()
                {
                    { () => SchemaName, (value) => dbObject.Schema.ToLower().Equals(value.ToLower()) },
                    { () => TableName, (value) => dbObject.Name.ToLower().Contains(value.ToLower()) },
                    { () => ColumnName, (value) => dbObject.Columns.Any(col => col.Name.ToLower().Contains(value.ToLower())) }
                };

                return criteria.Where(kp => !string.IsNullOrEmpty(kp.Key.Invoke())).All(kp => kp.Value.Invoke(kp.Key.Invoke()));
            }

            public static DbObjectSearch Parse(string text)
            {
                if (text.StartsWith("."))
                {
                    return new DbObjectSearch() { ColumnName = text.Substring(1).Trim() };
                }

                string[] parts = text.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                return
                    (parts.Length == 1) ? new DbObjectSearch() { TableName = parts[0] } :
                    (parts.Length == 2) ? new DbObjectSearch() { TableName = parts[0], ColumnName = parts[1] } :
                    (parts.Length > 2) ? new DbObjectSearch() { SchemaName = parts[0], TableName = parts[1], ColumnName = parts[2] } :
                    null;
            }
        }

        private void tvwObjects_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var tableNode = e.Node as ColumnContainerNode;
            if (tableNode != null)
            {
                if (tableNode.HasPlaceholder) tableNode.LoadColumns();
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_selectedTable != null)
            {
                rowCountToolStripMenuItem.Visible = true;
                rowCountToolStripMenuItem.Text = $"{_selectedTable.RowCount:n0} rows";
                removeAliasToolStripMenuItem.Visible = (_aliasManager.ContainsTable(_selectedTable.Table));
                buildInsertStatementToolStripMenuItem.Enabled = true;
                buildClassInitializerToolStripMenuItem.Enabled = true;
            }
            else
            {
                rowCountToolStripMenuItem.Visible = false;
                buildInsertStatementToolStripMenuItem.Enabled = false;
                buildClassInitializerToolStripMenuItem.Enabled = false;
            }

            createModelClassToolStripMenuItem.Enabled = _selectedObject?.SqlQueryEnabled ?? false;
        }

        private async void llRefresh_Click(object sender, EventArgs e)
        {
            _searchBox.Clear();
            await RefreshAsync();
        }

        public ResolveJoinResult ResolveJoin(string aliasList) => _joinResolver.Execute(aliasList);

        private void createModelClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelClassRequested?.Invoke(this, _selectedObject);
        }

        private void setAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedTable != null)
            {
                var dlg = new frmSetAlias();
                dlg.Table = _selectedTable.Table;
                dlg.AliasManager = _aliasManager;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _aliasManager.Aliases[dlg.Alias] = _selectedTable.Table.ToString();
                    _aliasManager.Save();
                    _selectedTable.Alias = dlg.Alias;
                }
            }
        }

        private void removeAliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedTable != null)
            {
                if (MessageBox.Show($"This will remove alias {_selectedTable.Alias} from table {_selectedTable.Table}", "Remove Alias", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _aliasManager.Aliases.Remove(_selectedTable.Alias);
                    _selectedTable.Alias = null;
                    _aliasManager.Save();
                }
            }
        }

        private void buildInsertStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var table = _selectedTable.Table;
                var columns = table.Columns.Where(col => !col.IsNullable && !col.Name.Equals(table.IdentityColumn));

                string sql = $@"INSERT INTO [{table.Schema}].[{table.Name}] (
                    {string.Join(", ", columns.Select(col => $"[{col.Name}]"))}
                ) VALUES (
                    {string.Join(", ", columns.Select(col => $"@{col.Name}"))}
                )";

                Clipboard.SetText(sql);
                MessageBox.Show("Insert statement copied to clipboard.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buildClassInitializerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var table = _selectedTable.Table;
                var columns = table.Columns.Where(col => !col.Name.Equals(table.IdentityColumn)).OrderBy(col => col.IsNullable);

                var csharp = $@"new {table.Name}() 
                {{
                    {string.Join(",\r\n", columns.Select(col => (!col.IsNullable) ? 
                        $"{col.Name} = /* required */" : 
                        $"/* {col.Name} = optional */"))}
                }}";

                Clipboard.SetText(csharp);
                MessageBox.Show("Class initializer statement copied to clipboard.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void viewDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmResolvedSQL dlg = new frmResolvedSQL();
            dlg.SQL = (_selectedObject.DbObject as IDefinition).SqlDefinition;
            dlg.ShowDialog();
        }

        private void getDbDiagramioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tableNodes = ((tvwObjects.CheckBoxes) ?
                    tvwObjects.FindNodesOfType<TableNode>().Where(nd => nd.Checked) :
                    tvwObjects.FindNodesOfType<TableNode>())                    
                    .ToArray();

                //selected tables or all tables
                var tables = tableNodes
                    .Where(node => node.DbObject?.Type == DbObjectType.Table)
                    .Select(node => node.DbObject as Table);

                // foreign keys (we'll figure out later which ones apply)
                var foreignKeys = _objects.OfType<ForeignKey>();

                var result = _diagramBuilder.GetSyntax(tables, foreignKeys);

                Clipboard.SetText(result.ToString());
                MessageBox.Show("Diagram syntax was copied to clipboard.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);                
            }
        }
    }
}

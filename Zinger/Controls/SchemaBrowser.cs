using SqlSchema.Library;
using SqlSchema.Library.Interfaces;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zinger.Controls.Nodes;
using Zinger.Forms;
using Zinger.Interfaces;
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
        private IDbObject _object;
        private bool _lineEndCommas = true;

        public SchemaBrowser()
        {            
            InitializeComponent();

            lineendCommasToolStripMenuItem.Checked = true;

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
                    .Where(obj => !string.IsNullOrEmpty(obj.Schema) && (search?.IsIncluded(obj) ?? true))
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

                    var procs = schemaGrp.OfType<Procedure>().OrderBy(obj => obj.Name);
                    if (procs.Any())
                    {
                        folderNode = new FolderNode("Procedures", procs.Count());
                        schemaNode.Nodes.Add(folderNode);
                        foreach (var proc in procs)
                        {
                            var procNode = new ProcedureNode(proc);
                            folderNode.Nodes.Add(procNode);
                        }
                    }

                    var synonyms = schemaGrp.OfType<Synonym>().OrderBy(obj => obj.Name);
                    if (synonyms.Any())
                    {
                        folderNode = new FolderNode("Synonyms", synonyms.Count());
                        schemaNode.Nodes.Add(folderNode);
                        foreach (var syn in synonyms)
                        {
                            var synNode = new SynonymNode(syn);
                            folderNode.Nodes.Add(synNode);
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
            _object = hitTest.Node as IDbObject;
            
            bool viewDef = false;
            if (_object != null)
            {
                viewDef = _object.DbObject is IDefinition;
            }

            viewDefinitionToolStripMenuItem.Enabled = viewDef;

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
                    { () => ColumnName, (value) => dbObject.Columns?.Any(col => col.Name.ToLower().Contains(value.ToLower())) ?? false }
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
                insertStatementToolStripMenuItem.Enabled = true;
                buildClassInitializerToolStripMenuItem.Enabled = true;
            }
            else
            {
                rowCountToolStripMenuItem.Visible = false;
                insertStatementToolStripMenuItem.Enabled = false;
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
                var columnGroups = table.Columns.Where(col => !col.Name.Equals(table.IdentityColumn)).ToLookup(col => col.IsNullable);

                var columns = new[]
                {
                    string.Join(", ", columnGroups[false].Select(col => $"[{col.Name}]")),
                    "/* " + string.Join(", ", columnGroups[true].Select(col => $"[{col.Name}]")) + " */"
                };

                var values = new[]
                {
                    string.Join(", ", columnGroups[false].Select(col => $"@{col.Name}")),
                    "/* " + string.Join(", ", columnGroups[true].Select(col => $"@{col.Name}")) + " */"
                };

                string sql = $@"INSERT INTO [{table.Schema}].[{table.Name}] (
                    {string.Join(", ", columns)}
                ) VALUES (
                    {string.Join(", ", values)}
                )";

                Clipboard.SetText(sql);
                MessageBox.Show("Insert statement copied to clipboard.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void updateStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var table = _selectedTable.Table;

                var columnGroups = table.Columns
                    .Where(col => !col.IsCalculated)
                    .ToLookup(col => col.Name.Equals(table.IdentityColumn));

                var setColumns = string.Join(", \r\n", columnGroups[false].Select(col => $"[{col.Name}] = @{col.Name}{IsOptional(col)}"));

                var identity = columnGroups[true].First().Name;
                var whereIdentity = $"[{identity}]=@{identity}";

                string sql = $@"UPDATE [{table.Schema}].[{table.Name}] SET
{setColumns} 
WHERE {whereIdentity}";

                Clipboard.SetText(sql);
                MessageBox.Show("Update statement copied to clipboard.");

                string IsOptional(Column col) => (col.IsNullable) ? " /* optional */" : string.Empty;
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
            dlg.SQL = (_object.DbObject as IDefinition).SqlDefinition;
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

        private void getTableVariableToolStripMenuItem_Click(object sender, EventArgs e) =>
            BuildSyntax(
                col => $"[{col.Name}] {col.TypeSyntax()} {col.NullableSyntax()}", 
                "Table variable syntax copied to clipboard.",
                (table, cols) => 
$@"DECLARE @{table.Name} TABLE (
{cols}
)");
        
        private void copyAllColumnNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedObject is null) throw new Exception("No object selected.");

                var alias = (_selectedObject is TableNode tableNode) ? tableNode.Alias : default;

                int length = 0;
                List<CopyColumnInfo> columns = new List<CopyColumnInfo>();
                _selectedObject.Columns.ForEach(col =>
                {
                    length += col.ColumnName.Length;
                    columns.Add(new CopyColumnInfo()
                    {
                        ColumnName = col.ColumnName,
                        TotalLength = length
                    });
                });

                // assuming 65 character lines...
                var output = string.Join(",\r\n", columns
                    .GroupBy(col => col.TotalLength / 65)
                    .Select(grp => string.Join(", ", grp.Select(item => (!string.IsNullOrEmpty(alias)) ? 
                        $"[{alias}].[{item.ColumnName}]" : 
                        $"[{item.ColumnName}]"))));

                Clipboard.SetText(output);
                MessageBox.Show("Column names copied to clipboard.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private class CopyColumnInfo
        {
            public string ColumnName { get; set; }
            public int TotalLength { get; set; }
        }

        private void paramDeclarationsToolStripMenuItem_Click(object sender, EventArgs e) =>
            BuildSyntax(col => $"@{col.Name} {col.TypeSyntax()}", "Param declarations copied to clipboard.");
        
        private void paramListToolStripMenuItem_Click(object sender, EventArgs e) => 
            BuildSyntax(col => $"@{col.Name}", "Param list copied to clipboard.");
        
        private void lineendCommasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lineEndCommas = !_lineEndCommas;
            lineendCommasToolStripMenuItem.Checked = _lineEndCommas;
        }

        private void BuildSyntax(Func<Column, string> columnTemplate, string message, Func<Table, string, string> outerTemplate = null)
        {
            try
            {
                var table = _selectedTable.Table;
                var separator = _lineEndCommas ? ",\r\n\t" : "\r\n\t,";
                var output = string.Join(separator, table.Columns.Select(columnTemplate));

                if (outerTemplate != null)
                {
                    output = outerTemplate.Invoke(table, output);
                }

                Clipboard.SetText(output);
                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }    
}

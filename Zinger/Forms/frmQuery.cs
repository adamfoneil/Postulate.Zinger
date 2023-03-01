using Excel2SqlServer.Library;
using JsonSettings;
using Microsoft.Data.SqlClient;
using SqlSchema.Library;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zinger.Controls.Nodes;
using Zinger.Interfaces;
using Zinger.Models;
using Zinger.Services;
using Zinger.Services.Providers;

namespace Zinger.Forms
{
    public partial class frmQuery : Form, ISaveable
    {
        public string Filename { get; private set; }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set
            {
                if (value != _isModified)
                {
                    if (value)
                    {
                        if (!Text.StartsWith("*")) Text = "*" + Text;
                    }
                    else
                    {
                        if (Text.StartsWith("*")) Text = Text.Substring(1);
                    }
                    _isModified = value;
                }
            }
        }

        public DataTable DataTable => queryEditor1.DataTable;

        public string DefaultExtension => ".pqh";

        public frmQuery()
        {
            InitializeComponent();
            schemaBrowser1.ModelClassRequested += SchemaBrowser1_ModelClassRequested;
            schemaBrowser1.SchemaInspected += OnSchemaInspected;
            queryEditor1.JoinResolutionRequested += QueryEditor1_JoinResolutionRequested;
        }

        private void QueryEditor1_JoinResolutionRequested(object sender, string e)
        {
            try
            {
                if (schemaBrowser1.IsSchemaSupported)
                {
                    var result = schemaBrowser1.ResolveJoin(e);
                    if (result.IsSuccessful)
                    {
                        queryEditor1.ReplaceSelection(result.FromClause);

                        if (result.UnrecognziedAliases.Any())
                        {
                            MessageBox.Show($"There are one or more unrecognized aliases in your selection: {string.Join(", ", result.UnrecognziedAliases)}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Schema browser not supported.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void OnSchemaInspected(object sender, EventArgs e)
        {
            btnSchema.Enabled = schemaBrowser1.IsSchemaSupported;
        }

        private void SchemaBrowser1_ModelClassRequested(object sender, ColumnContainerNode node)
        {
            var dlg = new frmCopyModelClass() { ObjectName = node.DbObject.ToString() };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var qcb = new QueryClassBuilder(queryEditor1.Provider.GetCommand);
                var schemaTable = queryEditor1.Provider.GetSchemaTable(node.SqlQuery);
                var modelClass = qcb.GetResultClass(schemaTable, node.ModelClassName, true, isResultClass: false, withAttributes: dlg.IncludeAttributes);
                Clipboard.SetText(modelClass);
                MessageBox.Show($"Model class {node.ModelClassName} copied to clipboard.");
            }
        }

        private void btnConnections_Click(object sender, EventArgs e)
        {
            try
            {
                frmContainer parent = MdiParent as frmContainer;
                if (parent != null)
                {
                    parent.ShowConnectionDialog();
                    FillConnectionDropdown();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void LoadQuery(string fileName)
        {
            var sq = queryEditor1.LoadQuery(fileName);
            cbConnection.SelectedIndex = cbConnection.FindString(sq.ConnectionName);
            resultClassBuilder1.QueryName = sq.Name;
            SetWindowTitle(fileName);
            Filename = fileName;
            IsModified = false;
        }

        private void SetWindowTitle(string name)
        {
            FindForm().Text = name;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                FillConnectionDropdown();
                FillScriptGen();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void FillScriptGen()
        {
            var allTypes = Assembly.GetExecutingAssembly()
                .GetExportedTypes();

            var scriptGenTypes = allTypes
                .Where(t => t.IsSubclassOf(typeof(ScriptGenBase)) && !t.IsAbstract);

            foreach (var type in scriptGenTypes)
            {
                var gen = Activator.CreateInstance(type) as ScriptGenBase;
                ToolStripMenuItem btn = new ToolStripMenuItem(gen.Title) { DisplayStyle = ToolStripItemDisplayStyle.Text };
                btn.Click += delegate (object sender, EventArgs args)
                {
                    try
                    {
                        gen.GenerateAndCopy(queryEditor1.Provider);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                    
                };

                btnScriptGen.DropDownItems.Add(btn);
            }
        }

        private void FillConnectionDropdown()
        {
            frmContainer parent = MdiParent as frmContainer;
            var connections = parent.GetSavedConnections();

            cbConnection.SelectedIndexChanged -= cbConnection_SelectedIndexChanged;
            cbConnection.Items.Clear();

            if (connections.Connections?.Any() ?? false)
            {
                foreach (SavedConnection sc in connections.Connections.OrderBy(sc => sc.Name)) cbConnection.Items.Add(sc);
            }

            cbConnection.SelectedIndexChanged += cbConnection_SelectedIndexChanged;
        }

        private async void cbConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConnection.SelectedItem != null)
            {
                SavedConnection sc = cbConnection.SelectedItem as SavedConnection;

                var providers = new Dictionary<ProviderType, QueryProvider>()
                {
                    [ProviderType.SqlServer] = new SqlServerQueryProvider(sc.ConnectionString),
                    [ProviderType.MySql] = new MySqlQueryProvider(sc.ConnectionString),
                    [ProviderType.OleDb] = new OleDbQueryProvider(sc.ConnectionString),
                    [ProviderType.SqlCe] = new SqlCeQueryProvider(sc.ConnectionString)
                };

                var provider = providers[sc.ProviderType];

                queryEditor1.Enabled = true;
                queryEditor1.Provider = provider;
                IsModified = true;

                frmContainer parent = MdiParent as frmContainer;
                parent.Options.ActiveConnection = sc.Name;

                await schemaBrowser1.FillAsync(sc.ProviderType, provider.GetConnection, sc.Name);
                if (splitContainer1.Panel2Collapsed) splitContainer1.Panel2Collapsed = !schemaBrowser1.IsSchemaSupported;

                btnScriptGen.Enabled = true;
            }
            else
            {
                queryEditor1.Enabled = false;
                btnScriptGen.Enabled = false;
            }

            btnSchema.Enabled = cbConnection.SelectedItem != null;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    queryEditor1.Execute();
                    break;

                case Keys.F3:
                case Keys.OemPeriod when e.Control:
                    if (cbConnection.SelectedItem != null)
                    {
                        if (splitContainer1.Panel2Collapsed) splitContainer1.Panel2Collapsed = false;
                        schemaBrowser1.Focus();
                    }
                    break;
            }
        }

        private void tbQueryName_TextChanged(object sender, EventArgs e)
        {
            queryEditor1.QueryName = resultClassBuilder1.QueryName;
        }

        private void queryEditor1_Executed(object sender, EventArgs e)
        {
            var results = sender as QueryProvider.ExecuteResult;
            resultClassBuilder1.ResultClass = results.ResultClass;
            resultClassBuilder1.QueryClass = results.QueryClass;
            Table table = FromDataTable(results.SchemaTable);
            resultClassBuilder1.TableVariable = SyntaxBuilder.GenerateTableVariable(table, resultClassBuilder1.PaddingBetweenNamesAndTypes, resultClassBuilder1.LineEndCommas);
            //queryEditor1.Provider.ResolvedQuery
        }

        private Table FromDataTable(DataTable schemaTable) => new Table()
        {
            Name = "tableVar",
            Columns = schemaTable.AsEnumerable().Select(row => new Column()
            {
                Name = row.Field<string>("ColumnName"),
                DataType = row.Field<string>("DataTypeName"),
                IsNullable = row.Field<bool>("AllowDbNull")
            }).ToArray()
        };
        

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            try
            {
                queryEditor1.Execute();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void resultClassBuilder1_QueryNameChanged(object sender, EventArgs e)
        {
            queryEditor1.QueryName = resultClassBuilder1.QueryName;
            resultClassBuilder1.RenameQuery(queryEditor1.QueryName);
            //SetWindowTitle(resultClassBuilder1.QueryName);
        }

        internal void SaveQuery(string fileName)
        {
            SavedQuery qry = new SavedQuery()
            {
                ConnectionName = cbConnection.SelectedItem.ToString(),
                Name = queryEditor1.QueryName,
                Sql = queryEditor1.Sql,
                Parameters = queryEditor1.Parameters
            };

            JsonFile.Save(fileName, qry);
            IsModified = false;
            Text = fileName;
        }

        private void frmQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsModified)
            {
                string fileName = (string.IsNullOrEmpty(Filename)) ? "new file" : Filename;
                var result = MessageBox.Show($"Save changes to {fileName}?", "Save Changes", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;

                    case DialogResult.No:
                        return;

                    case DialogResult.Yes:
                        if (!frmContainer.PromptSaveFileInner(this, out fileName))
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;

                }

                SaveQuery(fileName);
            }
        }

        private void queryEditor1_Modified(object sender, EventArgs e)
        {
            IsModified = true;
        }

        private async void btnDataToScript_Click(object sender, EventArgs e)
        {
            var analyzers = new Dictionary<ProviderType, Analyzer>()
            {
                { ProviderType.SqlServer, new SqlServerAnalyzer() }
            };

            try
            {
                var dlg = new frmScriptData();
                dlg.DataTable = queryEditor1.DataTable;

                var providerType = queryEditor1.Provider.ProviderType;
                if (analyzers.ContainsKey(providerType))
                {
                    using (var cn = queryEditor1.Provider.GetConnection())
                    {
                        dlg.DatabaseTables = (await analyzers[providerType].GetDbObjectsAsync(cn)).OfType<Table>();
                    }
                }

                dlg.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnSchema_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private async void btnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Excel Files|*.xlsx|All Files|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var importDlg = new frmImportExcel() { ExcelFile = dlg.FileName };
                    if (importDlg.ShowDialog() == DialogResult.OK)
                    {
                        using (var cn = queryEditor1.Provider.GetConnection())
                        {
                            var loader = new ExcelLoader();
                            await loader.SaveAsync(importDlg.ExcelFile, cn as SqlConnection, importDlg.Schema, importDlg.TableName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public async Task SaveAsync(string fileName)
        {
            SaveQuery(fileName);
            await Task.CompletedTask;
        }        
    }
}
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
using System.Windows.Forms;
using Zinger.Controls.Nodes;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmQuery : Form
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

        public frmQuery()
        {
            InitializeComponent();
            schemaBrowser1.ModelClassRequested += SchemaBrowser1_ModelClassRequested;
        }

        private void SchemaBrowser1_ModelClassRequested(object sender, ColumnContainerNode node)
        {
            var qcb = new QueryClassBuilder(queryEditor1.Provider.GetCommand);
            var schemaTable = queryEditor1.Provider.GetSchemaTable(node.SqlQuery);
            var modelClass = qcb.GetResultClass(schemaTable, node.ModelClassName, true, isResultClass: false);
            Clipboard.SetText(modelClass);
            MessageBox.Show($"Model class {node.ModelClassName} copied to clipboard.");
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
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
                foreach (SavedConnection sc in connections.Connections) cbConnection.Items.Add(sc);
            }
            
            cbConnection.SelectedIndexChanged += cbConnection_SelectedIndexChanged;
        }

        private async void cbConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConnection.SelectedItem != null)
            {
                SavedConnection sc = cbConnection.SelectedItem as SavedConnection;

                Dictionary<ProviderType, QueryProvider> providers = new Dictionary<ProviderType, QueryProvider>()
                {
                    { ProviderType.SqlServer, new SqlServerQueryProvider(sc.ConnectionString) },
                    { ProviderType.MySql, new MySqlQueryProvider(sc.ConnectionString) },
                    { ProviderType.OleDb, new OleDbQueryProvider(sc.ConnectionString) }
                };

                var provider = providers[sc.ProviderType];

                queryEditor1.Enabled = true;
                queryEditor1.Provider = provider;
                IsModified = true;

                frmContainer parent = MdiParent as frmContainer;
                parent.Options.ActiveConnection = sc.Name;

                await schemaBrowser1.FillAsync(sc.ProviderType, provider.GetConnection);
                if (splitContainer1.Panel2Collapsed) splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                queryEditor1.Enabled = false;
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
            //queryEditor1.Provider.ResolvedQuery
        }

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
    }
}
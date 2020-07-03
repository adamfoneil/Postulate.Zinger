﻿using JsonSettings;
using SqlSchema.Library;
using SqlSchema.Library.Models;
using SqlSchema.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Forms
{
    public partial class frmQuery : Form
    {
        public string Filename { get; private set; }
        public bool IsModified { get; private set; }

        public DataTable DataTable => queryEditor1.DataTable;

        public frmQuery()
        {
            InitializeComponent();
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
            SetWindowTitle(sq.Name);
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

        private void cbConnection_SelectedIndexChanged(object sender, EventArgs e)
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

                queryEditor1.Enabled = true;
                queryEditor1.Provider = providers[sc.ProviderType];
                IsModified = true;

                frmContainer parent = MdiParent as frmContainer;
                parent.Options.ActiveConnection = sc.Name;
            }
            else
            {
                queryEditor1.Enabled = false;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    queryEditor1.Execute();
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
            SetWindowTitle(resultClassBuilder1.QueryName);
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
    }
}
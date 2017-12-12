using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Zinger.Forms;
using Zinger.Models;

namespace Zinger
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnConnections_Click(object sender, EventArgs e)
        {
            frmConnections dlg = new frmConnections();
            dlg.SavedConnections = GetSavedConnections();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XmlSerializerHelper.Save(dlg.SavedConnections, SavedConnectionFilename());
                FillConnectionDropdown();
            }
        }

        public SavedConnections GetSavedConnections()
        {
            string fileName = SavedConnectionFilename();

            return (File.Exists(fileName)) ?
                XmlSerializerHelper.Load<SavedConnections>(SavedConnectionFilename()) :
                new SavedConnections();
        }

        private string SavedConnectionFilename()
        {
            string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Zinger", "SavedConnections.xml");
            string folder = Path.GetDirectoryName(result);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return result;
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
            cbConnection.SelectedIndexChanged -= cbConnection_SelectedIndexChanged;
            cbConnection.Items.Clear();
            foreach (SavedConnection sc in GetSavedConnections()) cbConnection.Items.Add(sc);
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
                    { ProviderType.MySql, new MySqlQueryProvider(sc.ConnectionString) }
                };

                queryEditor1.Enabled = true;
                queryEditor1.Provider = providers[sc.ProviderType];
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
            queryEditor1.QueryName = tbQueryName.Text;
        }

        private void queryEditor1_Executed(object sender, EventArgs e)
        {
            var results = sender as QueryProvider.ExecuteResult;
            tbResultClass.Text = results.ResultClass;
        }
    }
}
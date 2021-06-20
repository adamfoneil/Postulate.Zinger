using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmConnections : Form
    {
        public SavedConnections SavedConnections { get; internal set; }
        public string SavedConnectionFolder => SavedConnections.Filename;

        public Func<SavedConnection, (bool result, string message)> OnTestConnection;

        public frmConnections()
        {
            InitializeComponent();

            // see https://stackoverflow.com/a/34345439/2023653
            var cell = dataGridView1.TopLeftHeaderCell;

            dataGridView1.AutoGenerateColumns = false;
            colProvider.Fill<ProviderType>();
        }

        private void frmConnections_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = new BindingList<SavedConnection>(SavedConnections.Connections);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as BindingList<SavedConnection>;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // ignore
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                bool anyErrors = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    row.ErrorText = null;
                    SavedConnection sc = row.DataBoundItem as SavedConnection;
                    var result = OnTestConnection?.Invoke(sc) ?? (false, "Testing not hooked up");
                    if (!result.result)
                    {
                        anyErrors = true;
                        row.ErrorText = result.message;
                    }
                }

                if (!anyErrors)
                {
                    MessageBox.Show("All connections opened successfully.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FileSystem.OpenDocument(SavedConnectionFolder);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
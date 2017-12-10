using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Forms
{
    public partial class frmConnections : Form
    {
        private SavedConnections _data;

        private string SavedConnectionFilename()
        {
            string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Zinger", "SavedConnections.xml");
            string folder = Path.GetDirectoryName(result);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return result;
        }

        public frmConnections()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmConnections_Load(object sender, EventArgs e)
        {
            try
            {
                colProvider.FillFromEnum<ProviderType>();

                string fileName = SavedConnectionFilename();

                _data = (File.Exists(fileName)) ? 
                    XmlSerializerHelper.Load<SavedConnections>(SavedConnectionFilename()) :
                    new SavedConnections();
                                
                dataGridView1.DataSource = new BindingList<SavedConnection>(_data);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as BindingList<SavedConnection>;
            XmlSerializerHelper.Save(data, SavedConnectionFilename());
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
    }
}
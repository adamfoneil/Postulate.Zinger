using AdamOneilSoftware;
using System;
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
            cbConnection.Items.Clear();
            foreach (SavedConnection sc in GetSavedConnections()) cbConnection.Items.Add(sc);
        }
    }
}
using ClosedXML.Excel;
using JsonSettings.Library;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library.Models;
using Zinger.Models;

namespace Zinger.Forms
{
    public partial class frmContainer : Form
    {
        private Options _options = null;

        internal const string FileDialogFilter = "Postulate Query Helper|*.pqh|All Files|*.*";

        public frmContainer()
        {
            InitializeComponent();
        }

        public Options Options { get { return _options; } }

        private void frmContainer_Load(object sender, EventArgs e)
        {
            try
            {
                _options = SettingsBase.Load<Options>();
                _options.MainFormPosition?.Apply(this);

                NewQueryWindow();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private frmQuery NewQueryWindow()
        {
            frmQuery frm = new frmQuery();
            frm.MdiParent = this;
            frm.Show();
            return frm;
        }

        internal bool ShowConnectionDialog()
        {
            try
            {
                frmConnections dlg = new frmConnections();
                dlg.SavedConnections = GetSavedConnections();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.SavedConnections.Save();
                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            return false;
        }

        private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewQueryWindow();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void connectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowConnectionDialog();
        }

        public SavedConnections GetSavedConnections()
        {
            return SettingsBase.Load<SavedConnections>();
        }

        public static string SavedConnectionPath()
        {
            string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Postulate Query Helper");
            if (!Directory.Exists(result)) Directory.CreateDirectory(result);
            return result;
        }

        private string SavedConnectionFilename()
        {
            return Path.Combine(SavedConnectionPath(), "SavedConnections.xml");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuery activeQuery = ActiveMdiChild as frmQuery;
                if (activeQuery == null) throw new Exception("No query is open");
                if (PromptSaveFile(out string fileName)) activeQuery.SaveQuery(fileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuery activeQuery = ActiveMdiChild as frmQuery;
                if (activeQuery == null) throw new Exception("No query is open");

                string fileName;
                if (!PromptSaveFileInner(activeQuery, out fileName)) return;

                activeQuery.SaveQuery(fileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Returns true if a file name is set on the form or false if user cancels the Save As dialog
        /// </summary>
        public static bool PromptSaveFileInner(frmQuery form, out string fileName)
        {
            fileName = form.Filename;

            if (string.IsNullOrEmpty(fileName))
            {
                return PromptSaveFile(out fileName);
            }

            return true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (PromptOpenFile(out string fileName))
                {
                    var frm = NewQueryWindow();
                    frm.LoadQuery(fileName);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public static bool PromptSaveFile(out string fileName)
        {
            fileName = null;

            var dlg = new SaveFileDialog();
            dlg.Filter = FileDialogFilter;
            dlg.DefaultExt = "pqh";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
                return true;
            }

            return false;
        }

        private bool PromptOpenFile(out string fileName)
        {
            fileName = null;

            var dlg = new OpenFileDialog();
            dlg.Filter = FileDialogFilter;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
                return true;
            }

            return false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void frmContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _options.MainFormPosition = FormPosition.FromForm(this);
            _options.Save();
        }

        private void resultsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "xlsx";
            dlg.Filter = "Excel Files|*.xlsx|All Files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var wb = new XLWorkbook(XLEventTracking.Disabled);
                int index = 0;
                foreach (var qryForm in Application.OpenForms.OfType<frmQuery>())
                {
                    index++;
                    wb.AddWorksheet(qryForm.DataTable, $"Sheet{index}");
                }
                wb.SaveAs(dlg.FileName);
            }
        }

        private void dataMigratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmMigrationBuilder();
            frm.SavedConnections = GetSavedConnections();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
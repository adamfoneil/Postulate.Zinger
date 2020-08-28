using System;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmImportExcel : Form
    {
        public frmImportExcel()
        {
            InitializeComponent();
        }

        public string ExcelFile { get; set; }

        private void frmImportExcel_Load(object sender, EventArgs e)
        {
            tbExcelFile.Text = ExcelFile;
        }

        public string TableName { get { return tbTableName.Text; } }

        public string Schema { get { return tbSchema.Text; } }

        private void tbExcelFile_BuilderClicked(object sender, WinForms.Library.Controls.BuilderEventArgs e)
        {
            tbExcelFile.SelectFile("Excel Files|*.xlsx|All Files|*.*", e);
        }
    }
}

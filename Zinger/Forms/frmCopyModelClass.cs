using System;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmCopyModelClass : Form
    {
        public frmCopyModelClass()
        {
            InitializeComponent();
        }

        public string ObjectName { get; set; }
        public bool IncludeAttributes { get; private set; }

        private void frmCopyModelClass_Load(object sender, EventArgs e)
        {
            lblTableName.Text = ObjectName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IncludeAttributes = true;
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IncludeAttributes = false;
            DialogResult = DialogResult.OK;
        }
    }
}

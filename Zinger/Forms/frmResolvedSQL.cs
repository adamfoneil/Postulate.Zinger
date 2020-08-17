using System;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmResolvedSQL : Form
    {
        public frmResolvedSQL()
        {
            InitializeComponent();
        }

        public string SQL { get; set; }

        private void frmResolvedSQL_Load(object sender, EventArgs e)
        {
            tbSQL.Text = SQL;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbSQL.Text);
        }
    }
}

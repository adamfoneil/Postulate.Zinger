using System;
using System.Windows.Forms;
using Zinger.Forms;

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
            dlg.ShowDialog();
        }
    }
}
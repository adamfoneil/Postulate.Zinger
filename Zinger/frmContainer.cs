using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zinger
{
	public partial class frmContainer : Form
	{
		public frmContainer()
		{
			InitializeComponent();
		}

		private void frmContainer_Load(object sender, EventArgs e)
		{
			NewQueryWindow();
		}

		private void NewQueryWindow()
		{
			frmQuery frm = new frmQuery();
			frm.MdiParent = this;
			frm.Show();
		}

		private void frmContainer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.N && e.Control)
			{
				NewQueryWindow();
				e.Handled = true;
			}
		}
	}
}

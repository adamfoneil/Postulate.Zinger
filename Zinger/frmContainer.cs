using System;
using System.IO;
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
			var autoLoadFiles = frmQuery.AutoLoadFiles();
			foreach (string fileName in autoLoadFiles)
			{
				var form = NewQueryWindow();
				form.LoadQuery(fileName);
				File.Delete(fileName);
			}

			NewQueryWindow();
		}

		private frmQuery NewQueryWindow()
		{
			frmQuery frm = new frmQuery();
			frm.MdiParent = this;
			frm.Show();
			return frm;
		}

		private void frmContainer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.N && e.Control)
			{
				NewQueryWindow();
				e.Handled = true;
			}
		}

		private void frmContainer_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				int index = 0;
				foreach (var form in MdiChildren)
				{
					index++;
					var qryForm = form as frmQuery;
					if (qryForm != null) qryForm.AutoSave(index);
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}
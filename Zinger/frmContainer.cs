using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Zinger.Forms;
using Zinger.Models;

namespace Zinger
{
	public partial class frmContainer : Form
	{
		private Options _options = null;

		public frmContainer()
		{
			InitializeComponent();
		}

		public static IEnumerable<string> AutoLoadFiles()
		{
			if (!Directory.Exists(SavedConnectionPath())) return Enumerable.Empty<string>();
			return Directory.GetFiles(SavedConnectionPath(), "*.sql", SearchOption.TopDirectoryOnly);
		}

		private void frmContainer_Load(object sender, EventArgs e)
		{
			try
			{
				_options = UserOptionsBase.Load<Options>("Options.xml", this);
				_options.RestoreFormPosition(_options.MainFormPosition, this);
				_options.TrackFormPosition(this, (fp) => _options.MainFormPosition = fp);

				var autoLoadFiles = AutoLoadFiles();
				foreach (string fileName in autoLoadFiles)
				{
					var form = NewQueryWindow();
					form.LoadQuery(fileName);
					File.Delete(fileName);
				}

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
				dlg.SavedConnectionFolder = SavedConnectionPath();
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					XmlSerializerHelper.Save(dlg.SavedConnections, SavedConnectionFilename());
					return true;
				}				
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}

			return false;
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
			string fileName = SavedConnectionFilename();

			return (File.Exists(fileName)) ?
				XmlSerializerHelper.Load<SavedConnections>(SavedConnectionFilename()) :
				new SavedConnections();
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
				frmQuery activeQuery = ActiveForm as frmQuery;
				activeQuery?.SaveAs();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}
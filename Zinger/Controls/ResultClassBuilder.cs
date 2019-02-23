using AdamOneilSoftware;
using System;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Controls
{
	public partial class ResultClassBuilder : UserControl
	{
		public event EventHandler QueryNameChanged;

		public ResultClassBuilder()
		{
			InitializeComponent();
		}

		public string QueryName
		{
			get { return tbQueryName.Text; }
			set { tbQueryName.Text = value; }
		}

		public string ResultClass
		{
			get { return tbResultClass.Text; }
			set { tbResultClass.Text = value; }
		}

		public string QueryClass
		{
			get { return tbQueryClass.Text; }
			set { tbQueryClass.Text = value; }
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			try
			{
				switch (tabControl2.SelectedIndex)
				{
					case 0:
						Clipboard.SetText(ResultClass);
						return;

					case 1:
						Clipboard.SetText(QueryClass);
						return;
				}
			}
			catch (Exception exc)
			{
				// this fails sometimes for bizarre reasons
				MessageBox.Show(exc.Message);
			}
		}

		private void tbQueryName_TextChanged(object sender, EventArgs e)
		{
			QueryNameChanged?.Invoke(sender, e);
		}

		public void RenameQuery(string queryName)
		{
			ResultClass = ReplaceFirstLine(ResultClass, QueryProvider.ResultClassFirstLine(queryName));
			QueryClass = ReplaceFirstLine(QueryClass, QueryProvider.QueryClassFirstLine(queryName));
		}

		private string ReplaceFirstLine(string content, string newFirstLine)
		{
			string[] lines = content.Split('\n');
			lines[0] = newFirstLine;
			return string.Join("\n", lines);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Shell.ViewDocument("https://github.com/adamosoftware/Postulate");
		}
	}
}
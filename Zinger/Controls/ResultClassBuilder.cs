using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(ResultClass);
		}

		private void tbQueryName_TextChanged(object sender, EventArgs e)
		{
			QueryNameChanged?.Invoke(sender, e);
		}

		public void RenameQuery(string queryName)
		{
			string[] lines = ResultClass.Split('\n');
			lines[0] = QueryProvider.ResultClassFirstLine(queryName);
			ResultClass = string.Join("\n", lines);
		}
	}
}

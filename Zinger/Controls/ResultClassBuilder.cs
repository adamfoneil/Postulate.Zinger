using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zinger.Controls
{
	public partial class ResultClassBuilder : UserControl
	{
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
	}
}

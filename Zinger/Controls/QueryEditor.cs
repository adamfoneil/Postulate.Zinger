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
    public partial class QueryEditor : UserControl
    {
        public QueryEditor()
        {
            InitializeComponent();
        }

        private void chkParams_CheckedChanged(object sender, EventArgs e)
        {
            splcQueryAndParams.Panel2Collapsed = !chkParams.Checked;
        }
    }
}

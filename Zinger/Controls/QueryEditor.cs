using System;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Controls
{
    public partial class QueryEditor : UserControl
    {
        private QueryProvider _queryProvider;

        public QueryEditor()
        {
            InitializeComponent();
        }

        public QueryProvider Provider
        {
            get
            {
                if (_queryProvider == null) throw new NullReferenceException("Please set the QueryEditor.Provider property.");
                return _queryProvider;
            }
            set
            {
                _queryProvider = value;
                dgvParams.DataSource = _queryProvider.Parameters;
            }
        }

        public void Execute()
        {
            try
            {
                tslQueryMetrics.Text = "Executing...";
                var results = Provider.Execute(tbQuery.Text);
                tslQueryMetrics.Text = $"{results.Rows.Count} records, {_queryProvider.Milleseconds}ms";
                dgvResults.DataSource = results;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }            
        }

        private void chkParams_CheckedChanged(object sender, EventArgs e)
        {
            splcQueryAndParams.Panel2Collapsed = !chkParams.Checked;
        }
    }
}
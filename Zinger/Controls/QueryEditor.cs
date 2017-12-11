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

        public new bool Enabled
        {
            get { return tbQuery.Enabled; }
            set
            {
                tbQuery.Enabled = value;
                dgvParams.Enabled = value;
            }
        }

        public string QueryName { get; set; }

        public QueryProvider Provider
        {
            get
            {                
                return _queryProvider;
            }
            set
            {
                _queryProvider = value;
                dgvParams.DataSource = _queryProvider?.Parameters;
            }
        }

        public void Execute()
        {
            if (!Enabled) return;

            try
            {
                pbExecuting.Visible = true;
                tslQueryMetrics.Text = "Executing...";
                var results = Provider.Execute(tbQuery.Text, QueryName);
                tslQueryMetrics.Text = $"{results.Rows.Count} records, {_queryProvider.Milleseconds}ms";
                dgvResults.DataSource = results;
            }
            catch (Exception exc)
            {
                tslQueryMetrics.Text = $"Error: {exc.Message}";
                MessageBox.Show(exc.Message);
            }   
            finally
            {
                pbExecuting.Visible = false;
            }
        }

        private void chkParams_CheckedChanged(object sender, EventArgs e)
        {
            splcQueryAndParams.Panel2Collapsed = !chkParams.Checked;
        }
    }
}
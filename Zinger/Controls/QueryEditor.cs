using AdamOneilSoftware;
using System;
using System.Data;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Controls
{
    public partial class QueryEditor : UserControl
    {        
        public event EventHandler Executed;

        private QueryProvider _provider;

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
            get { return _provider; }
            set
            {
                _provider = value;
                dgvParams.DataSource = _provider?.Parameters;
            }
        }

        public void Execute()
        {
            if (!Enabled) return;

            try
            {
                pbExecuting.Visible = true;
                tslQueryMetrics.Text = "Executing...";
                var result = Provider.Execute(tbQuery.Text, QueryName);
                tslQueryMetrics.Text = $"{result.DataTable.Rows.Count} records, {Provider.Milleseconds:n0}ms";
                dgvResults.DataSource = result.DataTable;
                Executed?.Invoke(result, new EventArgs());
            }
            catch (Exception exc)
            {
                tslQueryMetrics.Text = $"Error: {exc.Message}";
				string fullMessage = GetFullError(exc);
                MessageBox.Show(fullMessage);
            }   
            finally
            {
                pbExecuting.Visible = false;
            }
        }

		private string GetFullError(Exception exc)
		{
			string result = exc.Message;

			Exception inner = exc.InnerException;
			while (inner != null)
			{
				result += "\r\n- " + inner.Message;
				inner = inner.InnerException;
			}
			
			return result;
		}

		private void chkParams_CheckedChanged(object sender, EventArgs e)
        {
            splcQueryAndParams.Panel2Collapsed = !chkParams.Checked;
        }

        private void QueryEditor_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            colParamType.FillFromEnum<DbType>();            
        }

        private void dgvParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // ignore
        }
    }
}
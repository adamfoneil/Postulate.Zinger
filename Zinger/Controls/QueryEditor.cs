using JsonSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library.Extensions;
using Zinger.Models;

namespace Zinger.Controls
{
    public partial class QueryEditor : UserControl
    {
        public event EventHandler Executed;

        public event EventHandler Modified;

        public QueryEditor()
        {
            InitializeComponent();
            colParamType.Fill<DbType>();
            dgvParams.AutoGenerateColumns = false;
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

        public string Sql { get { return tbQuery.Text; } set { tbQuery.Text = value; } }

        public List<QueryProvider.Parameter> Parameters
        {
            get { return (dgvParams.DataSource as BindingList<QueryProvider.Parameter>)?.ToList(); }
            set { dgvParams.DataSource = ParametersFromEnumerable(value); }
        }

        public QueryProvider Provider { get; set; }
        public DataTable DataTable => dgvResults.DataSource as DataTable;

        public void Execute()
        {
            if (!Enabled) return;

            try
            {
                pbExecuting.Visible = true;
                tslQueryMetrics.Text = "Executing...";
                var result = Provider.Execute(tbQuery.Text, QueryName, Parameters);
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

        private void QueryEditor_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;                        

            dgvParams.DataSource = new BindingList<QueryProvider.Parameter>();            
        }

        private void dgvParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // ignore
            //Debugger.Break();
        }

        private QueryProvider.Parameter[] ParametersToArray(BindingList<QueryProvider.Parameter> parameters)
        {
            if (parameters == null) return Enumerable.Empty<QueryProvider.Parameter>().ToArray();
            return new List<QueryProvider.Parameter>(parameters).ToArray();
        }

        private BindingList<QueryProvider.Parameter> ParametersFromEnumerable(IEnumerable<QueryProvider.Parameter> parameters)
        {            
            BindingList<QueryProvider.Parameter> results = new BindingList<QueryProvider.Parameter>();
            if (parameters == null) return results;
            foreach (var p in parameters) results.Add(p);
            return results;
        }

        public SavedQuery LoadQuery(string fileName)
        {
            var sq = JsonFile.Load<SavedQuery>(fileName);
            tbQuery.Text = sq.Sql;
            dgvParams.DataSource = ParametersFromEnumerable(sq.Parameters);
            return sq;
        }

        private void tbQuery_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            Modified?.Invoke(sender, new EventArgs());
        }

        private void dgvParams_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Modified?.Invoke(sender, new EventArgs());
        }

        private void dgvParams_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Modified?.Invoke(sender, new EventArgs());
        }

        private void dgvParams_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Modified?.Invoke(sender, new EventArgs());
        }

        private void tbQuery_TextChanging(object sender, FastColoredTextBoxNS.TextChangingEventArgs e)
        {
            Modified?.Invoke(sender, new EventArgs());
        }

        private void dgvParams_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[nameof(colParamType)].Value = DbType.Int32;
        }
    }
}
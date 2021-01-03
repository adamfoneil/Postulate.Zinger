using JsonSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library.Extensions;
using Zinger.Forms;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Controls
{
    public partial class QueryEditor : UserControl
    {
        public event EventHandler Executed;
        public event EventHandler<string> JoinResolutionRequested;
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

        public string SelectedText { get => tbQuery.SelectedText; }

        public List<Parameter> Parameters
        {
            get { return (dgvParams.DataSource as BindingList<Parameter>)?.ToList(); }
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

                string sql = (tbQuery.Selection.Length > 0) ? tbQuery.Selection.Text : tbQuery.Text;

                var result = Provider.Execute(sql, QueryName, Parameters);
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
                tslResolvedSQL.Visible = true;
            }
        }

        public void ReplaceSelection(string text)
        {
            tbQuery.SelectedText = text;
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

            dgvParams.DataSource = new BindingList<Parameter>();
        }

        private void dgvParams_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // ignore
            //Debugger.Break();
        }

        private Parameter[] ParametersToArray(BindingList<Parameter> parameters)
        {
            if (parameters == null) return Enumerable.Empty<Parameter>().ToArray();
            return new List<Parameter>(parameters).ToArray();
        }

        private BindingList<Parameter> ParametersFromEnumerable(IEnumerable<Parameter> parameters)
        {
            BindingList<Parameter> results = new BindingList<Parameter>();
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

        private void InvokeModified(object sender)
        {
            tslResolvedSQL.Visible = false;
            Modified?.Invoke(sender, new EventArgs());
        }

        private void tbQuery_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            InvokeModified(sender);
        }

        private void dgvParams_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            InvokeModified(sender);
        }

        private void dgvParams_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            InvokeModified(sender);
        }

        private void dgvParams_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            InvokeModified(sender);
        }

        private void tbQuery_TextChanging(object sender, FastColoredTextBoxNS.TextChangingEventArgs e)
        {
            InvokeModified(sender);
        }

        private void dgvParams_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[nameof(colParamType)].Value = DbType.Int32;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            var frm = new frmResolvedSQL();
            frm.SQL = Provider.ResolvedQuery;
            frm.ShowDialog();
        }

        private void tbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J && e.Control && tbQuery.SelectedText.Length > 0)
            {
                JoinResolutionRequested?.Invoke(this, tbQuery.SelectedText);
            }
        }
    }
}
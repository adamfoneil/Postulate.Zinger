using SqlIntegration.Library;
using SqlIntegration.Library.Classes;
using SqlSchema.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmScriptData : Form
    {
        public frmScriptData()
        {
            InitializeComponent();
        }

        public DataTable DataTable { get; set; }
        public IEnumerable<Table> DatabaseTables { get; set; }

        private void frmScriptData_Load(object sender, EventArgs e)
        {
            if (DatabaseTables?.Any() ?? false)
            {
                cbTable.Items.AddRange(DatabaseTables.OrderBy(row => row.Name).ToArray());
            }
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var table = cbTable.SelectedItem as Table;
                if (table != null)
                {
                    var output = await BulkInsert.GetSqlStatementsAsync(table.Name, DataTable, new BulkInsertOptions()
                    {
                        SkipIdentityColumn = (rbOmitIdentity.Checked) ? table.IdentityColumn : null,
                        IdentityInsert = rbIdentityInsert.Checked
                    });

                    Clipboard.SetText(output.ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}

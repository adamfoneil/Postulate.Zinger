using System;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmChartBuilder : Form
    {
        public frmChartBuilder()
        {
            InitializeComponent();
        }

        private void frmChartBuilder_Load(object sender, EventArgs e)
        {
            var openQueries = Application.OpenForms.OfType<frmQuery>();
            foreach (var qry in openQueries) cbQuerySource.Items.Add(new QuerySource(qry));
        }

        private void btnRefreshChart_Click(object sender, EventArgs e)
        {
            try
            {
                var data = (cbQuerySource.SelectedItem as QuerySource).QueryForm.DataTable;
                var config = GetConfig();
                ChartBuilder.Execute(data, config, chart1);                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private ChartConfiguration GetConfig()
        {
            return
                (tabControl1.SelectedIndex == 0) ? chartConfig.SelectedObject as ChartConfiguration :
                (tabControl1.SelectedIndex == 1) ? JsonSerializer.Deserialize<ChartConfiguration>(tbConfig.Text) :
                throw new Exception("Unknown config source");
        }
    }
}

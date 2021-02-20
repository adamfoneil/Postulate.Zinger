using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions.ComboBoxes;
using Zinger.Models;

namespace Zinger.Forms
{
    public partial class frmMigrationBuilder : Form
    {
        JsonSDI<DataMigration> _doc = new JsonSDI<DataMigration>(".json", "Json Files|*.json", "Save changes?");        

        public frmMigrationBuilder()
        {
            InitializeComponent();
            dgvSteps.AutoGenerateColumns = false;
            dgvColumns.AutoGenerateColumns = false;
        }

        public SavedConnections SavedConnections { get; set; }

        private void frmMigrationBuilder_Load(object sender, System.EventArgs e)
        {
            InitBinding();
        }

        private void InitBinding()
        {
            _doc.Document = new DataMigration();
            _doc.SavingFile += delegate (object sender, EventArgs args)
            {
                _doc.Document.Steps = ((dgvSteps.DataSource as BindingSource).DataSource as BindingList<DataMigration.Step>).ToArray();
            };

            _doc.FileOpened += delegate (object sender, EventArgs args)
            {
                InitStepsDataGridView();
            };

            _doc.Controls.AddItems(cbSourceConnection,
                setProperty: (dm) => dm.SourceConnection = (cbSourceConnection.SelectedItem as SavedConnection).Name,
                setControl: (dm) => cbSourceConnection.Set<SavedConnection>(item => item.Name.Equals(dm.SourceConnection)),
                SavedConnections.Connections);

            _doc.Controls.AddItems(cbDestConnection,
                setProperty: (dm) => dm.DestConnection = (cbDestConnection.SelectedItem as SavedConnection).Name,
                setControl: (dm) => cbDestConnection.Set<SavedConnection>(item => item.Name.Equals(dm.DestConnection)),
                SavedConnections.Connections);

            void InitStepsDataGridView()
            {
                BindingSource bsSteps = new BindingSource();
                bsSteps.DataSource = new BindingList<DataMigration.Step>((_doc.Document?.Steps.OrderBy(row => row.Order) ?? Enumerable.Empty<DataMigration.Step>()).ToList());
                dgvSteps.DataSource = bsSteps;

                tbSelectFrom.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.SourceFromWhere)));

                BindingSource bsColumns = new BindingSource();
                bsColumns.DataSource = bsSteps;
                //bsColumns.DataMember = ""
            }
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            await _doc.SaveAsync();
        }

        private async void btnOpen_Click(object sender, System.EventArgs e)
        {
            await _doc.PromptOpenAsync();            
        }

        private async void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            await _doc.NewAsync();
        }
    }
}

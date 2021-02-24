using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions.ComboBoxes;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmMigrationBuilder : Form
    {
        JsonSDI<DataMigration> _doc = new JsonSDI<DataMigration>(".json", "Json Files|*.json", "Save changes?");
        
        private DataMigrator _migrator;

        public frmMigrationBuilder()
        {
            InitializeComponent();
            dgvSteps.AutoGenerateColumns = false;
            dgvColumns.AutoGenerateColumns = false;
            dgvParams.AutoGenerateColumns = false;
        }

        public SavedConnections SavedConnections { get; set; }

        private void frmMigrationBuilder_Load(object sender, EventArgs e)
        {
            _migrator = new DataMigrator(SavedConnections);

            InitBinding();
        }

        private void InitBinding()
        {
            _doc.Document = new DataMigration();
            _doc.SavingFile += delegate (object sender, EventArgs args)
            {
                _doc.Document.Steps = ((dgvSteps.DataSource as BindingSource).DataSource as BindingList<DataMigration.Step>).ToArray();
                _doc.Document.Parameters = ((dgvParams.DataSource as BindingSource).DataSource as BindingList<DataMigration.Parameter>).ToArray();
            };

            _doc.FileOpened += delegate (object sender, EventArgs args)
            {
                InitStepsDataGridView();
                InitParamsDataGridView();
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
                bsSteps.CurrentItemChanged += async delegate (object sender, EventArgs args)
                {
                    var messages = await _migrator.ValidateStepAsync(bsSteps.Current as DataMigration.Step, _doc.Document);

                    dgvSteps.CurrentRow.ErrorText = (messages.Any()) ? "There are one or more error in this step." : null;

                    if (messages.Any())
                    {
                        foreach (DataGridViewRow row in dgvColumns.Rows)
                        {
                            if (row.IsNewRow) continue;
                            var key = (row.DataBoundItem as DataMigration.Column).Key;
                            if (messages.Contains(key))
                            {
                                row.ErrorText = string.Join("\r\n", messages[key]);
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dgvColumns.Rows) row.ErrorText = null;
                    }
                };

                tbSelectFrom.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.SourceFromWhere)));
                tbSourceIdentityCol.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.SourceIdentityColumn)));
                tbDestIdentityCol.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.DestIdentityColumn)));

                BindingSource bsColumns = new BindingSource();
                bsColumns.DataSource = bsSteps;
                bsColumns.DataMember = nameof(DataMigration.Step.Columns);
                dgvColumns.DataSource = bsColumns;
            }

            void InitParamsDataGridView()
            {
                BindingSource bsParams = new BindingSource();
                var data = _doc.Document?.Parameters?.ToList() ?? new List<DataMigration.Parameter>();
                bsParams.DataSource = new BindingList<DataMigration.Parameter>(data);
                dgvParams.DataSource = bsParams;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await _doc.SaveAsync();
        }

        private async void btnOpen_Click(object sender, EventArgs e)
        {
            await _doc.PromptOpenAsync();            
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            await _doc.NewAsync();
        }

        private async void btnBuildColumns_Click(object sender, EventArgs e)
        {
            try
            {
                await _migrator.AddColumnsAsync(_doc.Filename, overwrite: true, _doc.Document.GetParameters());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }            
        }

        private async void frmMigrationBuilder_FormClosing(object sender, FormClosingEventArgs e)
        {
            await _doc.FormClosingAsync(e);
        }
    }
}

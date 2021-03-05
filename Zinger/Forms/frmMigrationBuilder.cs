using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
        private DataMigrator.MigrationResult _migrationResult;

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
                _migrator.CurrentFilename = _doc.Filename;
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
                bsSteps.CurrentItemChanged += delegate (object sender, EventArgs args)
                {
                    pbValidation.Image = null;
                    lblStepResult.Text = null;
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

        private async void btnValidateStep_Click(object sender, EventArgs e)
        {
            await RunMigrationTaskAsync(async (step, migration) => await _migrator.ValidateStepAsync(step, migration));
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            await RunMigrationTaskAsync(async (step, migration) => await _migrator.RunStepAsync(step, migration));
        }

        private async Task RunMigrationTaskAsync(Func<DataMigration.Step, DataMigration, Task<DataMigrator.MigrationResult>> func)
        {
            try
            {
                var step = (dgvSteps.DataSource as BindingSource).Current as DataMigration.Step;

                pbValidation.Image = imageList1.Images["loading"];
                _migrationResult = await func.Invoke(step, _doc.Document);
                pbValidation.Image = (_migrationResult.Success) ? imageList1.Images["success"] : imageList1.Images["fail"];
                lblStepResult.Text = $"{_migrationResult.Message} {_migrationResult.RowsCopied:n0} rows {_migrationResult.Action}";
            }
            catch (Exception exc)
            {
                pbValidation.Image = imageList1.Images["fail"];
                lblStepResult.Text = exc.Message;                
            }
            finally
            {
                llSourceSql.Enabled = !string.IsNullOrEmpty(_migrationResult?.SourceSql);
                llInsertSql.Enabled = !string.IsNullOrEmpty(_migrationResult?.InsertSql);
            }
        }

        private void SaveToClipboard(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void llSourceSql_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveToClipboard(_migrationResult.SourceSql);
        }

        private void llInsertSql_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveToClipboard(_migrationResult.InsertSql);
        }

        private async void btnAddStepColumns_Click(object sender, EventArgs e)
        {
            var step = (dgvSteps.DataSource as BindingSource).Current as DataMigration.Step;
            await _migrator.AddStepColumnsAsync(_doc.Document, step);
        }
    }
}

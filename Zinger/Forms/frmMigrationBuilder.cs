using SqlIntegration.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions.ComboBoxes;
using Zinger.Interfaces;
using Zinger.Models;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmMigrationBuilder : Form, ISaveable
    {
        JsonSDI<DataMigration> _doc = new JsonSDI<DataMigration>(".json", "Json Files|*.json", "Save changes?");
        
        private DataMigrator _migrator;
        private DataMigrator.MigrationResult _migrationResult;        

        public frmMigrationBuilder()
        {
            InitializeComponent();
            dgvSteps.AutoGenerateColumns = false;            
            dgvParams.AutoGenerateColumns = false;
        }

        public SavedConnections SavedConnections { get; set; }

        public string Filename => _doc.Filename;

        public string DefaultExtension => ".json";

        private void frmMigrationBuilder_Load(object sender, EventArgs e)
        {
            _migrator = new DataMigrator(SavedConnections);
            _migrator.Progress += ShowProgress;
            _migrator.ConsoleMessage += delegate (object sender2, string message)
            {
                consoleTextBox1.Insert(message);
            };

            InitBinding();
        }

        private void ShowProgress(object sender, SqlMigrator<int>.Progress e)
        {            
            pbMain.Value = e.PercentComplete;
            tslProgress.Text = $"{e.TotalRows:n0} total rows, {e.RowsMigrated:n0} migrated ({e.PercentComplete}%), {e.RowsSkipped:n0} skipped";
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
                SavedConnections.Connections.OrderBy(item => item.Name));

            _doc.Controls.AddItems(cbDestConnection,
                setProperty: (dm) => dm.DestConnection = (cbDestConnection.SelectedItem as SavedConnection).Name,
                setControl: (dm) => cbDestConnection.Set<SavedConnection>(item => item.Name.Equals(dm.DestConnection)),
                SavedConnections.Connections.OrderBy(item => item.Name));

            void InitStepsDataGridView()
            {
                BindingSource bsSteps = new BindingSource();
                bsSteps.DataSource = new BindingList<DataMigration.Step>((_doc.Document?.Steps.OrderBy(row => row.Order) ?? Enumerable.Empty<DataMigration.Step>()).ToList());
                dgvSteps.DataSource = bsSteps;
                bsSteps.CurrentItemChanged += delegate (object sender, EventArgs args)
                {
                    pbValidation.Image = null;
                    lblStepResult.Text = null;
                    var step = bsSteps.Current as DataMigration.Step;
                    if (step != null && step.Columns == null) step.Columns = new List<DataMigration.Column>();
                    propertyGrid1.SelectedObject = null;
                };

                migrationStep1.InitBinding(bsSteps);
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
            await _doc.SaveAsync();
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
                consoleTextBox1.Clear();
                tslProgress.Text = "Querying...";
                tslCancel.Visible = true;
                pbMain.Visible = true;                
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
                pbMain.Visible = false;
                tslProgress.Text = "Ready";
                tslCancel.Visible = false;
            }
        }

        private void SaveToClipboard(Func<string> getText)
        {
            try
            {
                var text = getText.Invoke();
                Clipboard.SetText(text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void llSourceSql_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveToClipboard(() => _migrationResult.SourceSql);
        }

        private void llInsertSql_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveToClipboard(() => _migrationResult.InsertSql);
        }

        private async void btnAddStepColumns_Click(object sender, EventArgs e)
        {
            try
            {
                var step = (dgvSteps.DataSource as BindingSource).Current as DataMigration.Step;
                await _migrator.AddStepColumnsAsync(_doc.Document, step);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnImportKeyMap_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _migrator.ImportKeyMapTableAsync(_doc.Document);
                MessageBox.Show($"Imported table {result.@object} with {result.rowCount:n0} rows.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public async Task SaveAsync(string fileName)
        {
            _doc.Filename = fileName;
            await _doc.SaveAsync();
        }

        private void tslCancel_Click(object sender, EventArgs e)
        {
            _migrator?.Cancel();
        }

        private async void btnRefreshProgress_Click(object sender, EventArgs e)
        {
            try
            {
                var step = (dgvSteps.DataSource as BindingSource).Current as DataMigration.Step;
                var progress = await _migrator.QueryMappingProgressAsync(_doc.Document, step);
                propertyGrid1.SelectedObject = progress;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }            
        }

        private void llImportKeyMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => btnImportKeyMap_Click(sender, new EventArgs());

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveToClipboard(() =>
            {
                var step = (dgvSteps.DataSource as BindingSource).Current as DataMigration.Step;
                return _migrator.GetUnmappedRowsQuery(step);
            });
        }
    }
}

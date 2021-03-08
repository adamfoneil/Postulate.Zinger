using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Controls
{
    public partial class MigrationStep : UserControl
    {
        public MigrationStep()
        {
            InitializeComponent();
            dgvColumns.AutoGenerateColumns = false;
        }

        public void InitBinding(BindingSource bsSteps)
        {
            tbSelectFrom.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.SourceFromWhere)));
            tbSourceIdentityCol.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.SourceIdentityColumn)));
            tbDestIdentityCol.DataBindings.Add(new Binding("Text", bsSteps, nameof(DataMigration.Step.DestIdentityColumn)));

            BindingSource bsColumns = new BindingSource();
            bsColumns.DataSource = bsSteps;
            bsColumns.DataMember = nameof(DataMigration.Step.Columns);
            dgvColumns.DataSource = bsColumns;
        }
    }
}

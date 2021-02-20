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
        }

        public SavedConnections SavedConnections { get; set; }

        private void frmMigrationBuilder_Load(object sender, System.EventArgs e)
        {
            _doc.Document = new DataMigration();

            _doc.Controls.AddItems(cbSourceConnection,                 
                setProperty: (dm) => dm.SourceConnection = (cbSourceConnection.SelectedItem as SavedConnection).Name,
                setControl: (dm) => cbSourceConnection.Set<SavedConnection>(item => item.Name.Equals(dm.SourceConnection)),
                SavedConnections.Connections);

            _doc.Controls.AddItems(cbDestConnection,
                setProperty: (dm) => dm.DestConnection = (cbDestConnection.SelectedItem as SavedConnection).Name,
                setControl: (dm) => cbDestConnection.Set<SavedConnection>(item => item.Name.Equals(dm.DestConnection)),
                SavedConnections.Connections);
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            await _doc.SaveAsync();
        }

        private async void btnOpen_Click(object sender, System.EventArgs e)
        {
            await _doc.PromptOpenAsync();            
        }
    }
}

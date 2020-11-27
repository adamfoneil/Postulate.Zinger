using SqlSchema.Library.Models;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmSetAlias : Form
    {
        public frmSetAlias()
        {
            InitializeComponent();
        }

        public Table Table { get; internal set; }

        public string Alias { get => textBox1.Text; }

        private void frmSetAlias_Load(object sender, System.EventArgs e)
        {
            label1.Text = $"Alias for Table: {Table}";
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            btnOK.Enabled = (textBox1.Text.Length > 0);
        }
    }
}

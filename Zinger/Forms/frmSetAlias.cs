using SqlSchema.Library.Models;
using System.Windows.Forms;
using Zinger.Services;

namespace Zinger.Forms
{
    public partial class frmSetAlias : Form
    {
        public frmSetAlias()
        {
            InitializeComponent();
        }

        public AliasManager AliasManager { get; internal set; }
        public Table Table { get; internal set; }

        public string Alias { get => textBox1.Text; }

        private void frmSetAlias_Load(object sender, System.EventArgs e)
        {
            label1.Text = $"Alias for Table: {Table}";
            chkOverwrite.Visible = false;
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (AliasManager.Aliases.ContainsKey(textBox1.Text))
            {
                chkOverwrite.Text = $"Overwrite alias on table {AliasManager.Aliases[textBox1.Text]}";
                chkOverwrite.Visible = true;
            }
            else
            {
                chkOverwrite.Visible = false;
            }

            EnableOKButton();
        }

        private void EnableOKButton()
        {
            btnOK.Enabled = (textBox1.Text.Length > 0 && (chkOverwrite.Visible && chkOverwrite.Checked || !chkOverwrite.Visible));
        }

        private void chkOverwrite_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableOKButton();
        }
    }
}

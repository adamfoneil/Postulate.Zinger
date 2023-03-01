using System;
using System.Windows.Forms;
using Zinger.Services;

namespace Zinger.Controls
{
    public partial class ResultClassBuilder : UserControl
    {
        public event EventHandler QueryNameChanged;

        public ResultClassBuilder()
        {
            InitializeComponent();
        }

        public string QueryName
        {
            get { return tbQueryName.Text; }
            set { tbQueryName.Text = value; }
        }

        public string ResultClass
        {
            get { return tbResultClass.Text; }
            set { tbResultClass.Text = value; }
        }

        public string QueryClass
        {
            get { return tbQueryClass.Text; }
            set { tbQueryClass.Text = value; }
        }

        public bool LineEndCommas => chkLineEndCommas.Checked;

        public bool PaddingBetweenNamesAndTypes => chkPadding.Checked;

        public string TableVariable { get => fctbTableVar.Text; set => fctbTableVar.Text = value; }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl2.SelectedIndex)
                {
                    case 0:
                        Clipboard.SetText(ResultClass);
                        return;

                    case 1:
                        Clipboard.SetText(QueryClass);
                        return;

                    case 3:
                        Clipboard.SetText(TableVariable);
                        return;
                }
            }
            catch (Exception exc)
            {
                // this fails sometimes for bizarre reasons
                MessageBox.Show(exc.Message);
            }
        }

        private void tbQueryName_TextChanged(object sender, EventArgs e)
        {
            QueryNameChanged?.Invoke(sender, e);
        }

        public void RenameQuery(string queryName)
        {
            ResultClass = ReplaceFirstLine(ResultClass, QueryClassBuilder.ResultClassFirstLine(queryName));
            QueryClass = ReplaceFirstLine(QueryClass, QueryClassBuilder.QueryClassFirstLine(queryName, true));
        }

        private string ReplaceFirstLine(string content, string newFirstLine)
        {
            string[] lines = content.Split('\n');
            lines[0] = newFirstLine;
            return string.Join("\n", lines);
        }
    }
}
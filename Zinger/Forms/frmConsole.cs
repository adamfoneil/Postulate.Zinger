using System.Linq;
using System.Windows.Forms;

namespace Zinger.Forms
{
    public partial class frmConsole : Form
    {
        public frmConsole()
        {
            InitializeComponent();
        }

        public int MaxLines { get; set; } = 100;

        public void Add(string text)
        {
            var lines = textBox1.Lines.ToList();
            lines.Insert(0, text);
            if (lines.Count > MaxLines) lines.RemoveRange(MaxLines, (lines.Count - MaxLines));
            textBox1.Lines = lines.ToArray();
        }
    }
}

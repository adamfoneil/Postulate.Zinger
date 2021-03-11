using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zinger.Controls
{
    public class ConsoleTextBox : TextBox
    {
        private List<string> _lines = new List<string>();

        public ConsoleTextBox()
        {
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            ReadOnly = true;
            Multiline = true;
            ScrollBars = ScrollBars.Vertical;
        }

        public int MaxLines { get; set; } = 100;

        public void Insert(string text)
        {
            _lines.Insert(0, text);
            if (_lines.Count > MaxLines) _lines.RemoveRange(100, _lines.Count - MaxLines);
            Text = string.Join("\r\n", _lines);
        }

        public new void Clear()
        {
            _lines = new List<string>();
            base.Clear();
        }
    }
}

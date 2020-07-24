using System;
using System.Windows.Forms;

namespace Zinger.Services
{
    public class TextBoxDelayHandler
    {
        private readonly TextBox _textBox;
        private readonly Timer _timer;

        public event EventHandler DelayedTextChanged;

        public TextBoxDelayHandler(TextBox textBox, int delayMs)
        {
            _textBox = textBox;
            _timer = new Timer() { Interval = delayMs };
            _timer.Tick += delegate (object sender, EventArgs e)
            {
                _timer.Stop();
                DelayedTextChanged?.Invoke(_textBox, e);                
            };

            _textBox.TextChanged += delegate (object sender, EventArgs e)
            {
                _timer.Start();
            };
        }
    }
}

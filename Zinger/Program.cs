using System;
using System.Windows.Forms;
using Zinger.Forms;

namespace Zinger
{
    internal static class Program
    {
        static frmConsole _console;

        [STAThread]
        private static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            try
            {
                _console = new frmConsole();
                Application.Run(new frmContainer());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Unhandled Exception");
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _console.Add(e.Exception.Message);
            _console.Show();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _console.Add((e.ExceptionObject as Exception).Message);
            _console.Show();
        }
    }
}
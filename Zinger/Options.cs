using JsonSettings.Library;
using WinForms.Library.Models;

namespace Zinger
{
    public class Options : SettingsBase
    {
        public FormPosition MainFormPosition { get; set; }
        public string ActiveConnection { get; set; }

        public override string Filename => BuildPath(System.Environment.SpecialFolder.ApplicationData, "Zinger", "settings.json");
    }
}
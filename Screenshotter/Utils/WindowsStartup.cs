using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter.Utils
{
    public static class WindowsStartup
    {
        private static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public const string AppName = "PipeScreenshot";

        public static bool Enabled
        {
            get => rkApp.GetValue(AppName) as string == Application.ExecutablePath;
            set
            {
                if (value)
                    rkApp.SetValue(AppName, Application.ExecutablePath);
                else
                    rkApp.DeleteValue(AppName, false);
            }
        }
    }
}

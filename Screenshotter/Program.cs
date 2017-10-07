using Screenshotter.Uploaders.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshotter
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += Application_ThreadException;

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            throw e.Exception;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw e.ExceptionObject as Exception;
        }
    }
}

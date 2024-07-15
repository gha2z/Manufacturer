using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntrManApp.WinBridge
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            //if (!IsRunAsAdmin())
            //{
            //    ProcessStartInfo startInfo = new ProcessStartInfo(Application.ExecutablePath)
            //    {
            //        UseShellExecute = true,
            //        Verb = "runas",
                  
            //        WorkingDirectory = Application.StartupPath
            //    };
            //    Process.Start(startInfo);
            //    Application.Exit();
            //    return;
            //}
            Application.Run(new Form1());
        }

        //static bool IsRunAsAdmin()
        //{
        //    WindowsIdentity id = WindowsIdentity.GetCurrent();
        //    WindowsPrincipal principal = new WindowsPrincipal(id);
        //    return principal.IsInRole(WindowsBuiltInRole.Administrator);
        //}
    }
}

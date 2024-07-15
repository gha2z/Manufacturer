using System.Diagnostics;
using System.Security.Principal;

namespace IntrManApp.SetupConfigurator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (!IsRunAsAdmin())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(Application.ExecutablePath)
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    Arguments = args.ToString(),
                    WorkingDirectory = Application.StartupPath
                };
                Process.Start(startInfo);
                Application.Exit();
                return;
            }
            bool unReg = args[0].ToLower().Trim().Contains("uninstall");
            Application.Run(new ConfigForm(unReg));
        }

        static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
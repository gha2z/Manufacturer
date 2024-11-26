using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text.Json;
using IntrManApp.Shared.Common;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;

namespace IntrManApp.SetupConfigurator
{
    public partial class ConfigForm : Form
    {
        bool sqlServerPassed = false;
        bool clientServerPassed = false;
        bool isUninstalling = false;
        bool isServerIncluded = false;
        bool isFinished = false;

        string logFile = string.Empty;
        string appDataPath = string.Empty;

        private StringBuilder sb = new();

        void Notify(string message)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                this.BringToFront();
                
                LogLabel.Text = message;

                try
                {
                    sb.AppendLine(DateTime.Now + ": " + message);
                    File.WriteAllText(logFile, sb.ToString());
                }
                catch { }
            }));
        }

        public ConfigForm(bool unReg)
        {
            InitializeComponent();
            isUninstalling = unReg;
            appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Gha2z ERP");
            if (!Directory.Exists(appDataPath)) Directory.CreateDirectory(appDataPath);
            var logPath = Path.Combine(appDataPath, "Installation");
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            logFile = Path.Combine(logPath, $"log{DateTime.Now.ToString("MMddyyyy_hhmmss")}.log");
        }

        private void ServerCk_CheckedChanged(object sender, EventArgs e)
        {
            isServerIncluded = ServerCk.Checked;
            ServerPanel.Enabled = ServerCk.Checked;
            ClientPanel.Enabled = !ServerCk.Checked;
            ServerTxt.ReadOnly = ServerCk.Checked;
            ClientServerPortNum.ReadOnly = ServerCk.Checked;
            if (ServerCk.Checked)
            {
                ClientCk.Checked = true;
                ServerTxt.Text = "localhost";
            }
        }

        private void UseIntSecTxt_CheckedChanged(object sender, EventArgs e)
        {
            UserIdTxt.ReadOnly = UseIntSecCk.Checked;
            PasswordTxt.ReadOnly = UseIntSecCk.Checked;
            if (UseIntSecCk.Checked)
            {
                UserIdTxt.Clear();
                PasswordTxt.Clear();
            }
        }
        private void ServerPortNum_ValueChanged(object sender, EventArgs e)
        {
            if (ServerCk.Checked) ClientServerPortNum.Value = ServerPortNum.Value;
        }

        private void TestServerBtn_Click(object sender, EventArgs e)
        {
            sqlServerPassed = false;
            clientServerPassed = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

            if (ServerCk.Checked)
            {
               var conString = $@"Server=.\{SqlServerTxt.Text};Database=Master;User Id={UserIdTxt.Text};Password={PasswordTxt.Text};" +
                    $"TrustServerCertificate=True;Integrated Security={UseIntSecCk.Checked}";
                using (SqlConnection conn = new(conString))
                {
                    try
                    {
                        Notify("Connecting to SQL Server...");
                        conn.Open();
                        Notify("Connection to SQL Server successful");
                        sqlServerPassed = true;
                    }
                    catch (Exception ex)
                    {
                        Notify(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else sqlServerPassed = true;

            if (ClientCk.Enabled)
            {
                Ping ping = new();
                try
                {
                    Notify("Pinging server ...");
                    PingReply reply = ping.Send(ServerTxt.Text);
                    if (reply.Status == IPStatus.Success)
                    {
                        clientServerPassed = true;
                        Notify("Server is reachable");
                    }
                }
                catch (Exception ex)
                {
                    Notify(ex.Message);
                }
            }
            else clientServerPassed = true;

            pictureBox1.Visible = ServerCk.Checked;
            pictureBox2.Visible = true;

            if (sqlServerPassed)
                pictureBox1.Image = Properties.Resources._32Check;
            else
                pictureBox1.Image = Properties.Resources.button_cancel;

            if (sqlServerPassed)
                pictureBox2.Image = Properties.Resources._32Check;
            else
                pictureBox2.Image = Properties.Resources.button_cancel;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            if (isUninstalling)
            {
                this.Height = 219;
            }
            else
            {
                this.Height = 700;
            }
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width-10, Screen.PrimaryScreen.WorkingArea.Height-this.Height-10);
            TestServerBtn.Visible = !isUninstalling;
            ContinueBtn.Visible = !isUninstalling;
            ServerCk.Checked = true;
            if (isUninstalling)
            {
                pictureBox3.Visible = true;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string svcName = "Gha2z ERP Backend Service";
            string path = new DirectoryInfo(Application.StartupPath).Parent?.FullName ?? Application.StartupPath;
            string binPath = Path.Combine(appDataPath, "Backend Service");  //Path.Combine(path, "Backend Service", "IntrManApp.Api.exe");
            ProcessStartInfo pi;
            Process proc;
            string jsonContent = string.Empty;

            try
            {

                if (!isUninstalling)
                {
                    if (isServerIncluded)
                    {
                        if(Directory.Exists(binPath)) Directory.Delete(binPath, true);
                        if (!Directory.Exists(binPath)) Directory.CreateDirectory(binPath);
                      

                        Notify("Extracting Backend Service ...");
                        ZipFile.ExtractToDirectory(Path.Combine(Application.StartupPath, "backend.zip"), binPath, true);


                        Notify("Creating Backend Service configuration file ...");
                        var appSettings = new ServerAppSettings();
                        appSettings.Port = (int)ServerPortNum.Value;
                        appSettings.ConnectionStrings.Database =
                            $"Server={Path.Combine(".",SqlServerTxt.Text)};Database=Gha2zERPDB;User Id={UserIdTxt.Text};Password={PasswordTxt.Text};" +
                            $"TrustServerCertificate=True;Integrated Security={UseIntSecCk.Checked}";
                        appSettings.ConnectionStrings.DbConfig =
                            $"Server={Path.Combine(".", SqlServerTxt.Text)};Database=Master;User Id={UserIdTxt.Text} ;Password= {PasswordTxt.Text};" +
                            $"TrustServerCertificate=True;Integrated Security={UseIntSecCk.Checked}";
                        var appSettingFile = Path.Combine(binPath, "appsettings.json");
                        jsonContent = JsonSerializer.Serialize<ServerAppSettings>(appSettings);
                        File.WriteAllText(appSettingFile, jsonContent);
                        Notify($"Write config: {Environment.NewLine}{jsonContent}{Environment.NewLine} to {appSettingFile}");

                        Notify("Creating Intrman Backend Windows Service ...");
                        pi = new ProcessStartInfo("sc.exe",
                                $"create \"{svcName}\" binPath=\"{Path.Combine(binPath, "IntrManApp.Api.exe")}\" start=auto");
                        pi.CreateNoWindow = true;
                        proc = Process.Start(pi);
                        proc?.WaitForExit();

                        Notify("Registering Intrman Backend Windows Service");
                        pi = new ProcessStartInfo("sc.exe", $"description \"{svcName}\" \"Gha2z ERP Backend Service\"");
                        proc = Process.Start(pi);
                        proc?.WaitForExit();

                        Notify("Configuring Intrman Backend Windows Service recovery options");
                        pi = new ProcessStartInfo("sc.exe", $"failure  \"{svcName}\" reset=0 actions=restart/3950/restart/3950/restart/3950");
                        proc = Process.Start(pi);
                        proc?.WaitForExit();

                        Notify("Starting Intrman Backend windows service");
                        pi = new ProcessStartInfo("sc.exe", $"start \"{svcName}\"");
                        Process.Start(pi);
                    }

                    binPath = Path.Combine(appDataPath, "Client App");
                    if (!Directory.Exists(binPath))
                    {
                        Notify("Creating Client App data folder ...");
                        Directory.CreateDirectory(binPath);
                    }

                    Notify("Extracting Client App ...");
                    ZipFile.ExtractToDirectory(Path.Combine(Application.StartupPath, "app.zip"), binPath, true);

                    var settings = new ClientAppSettingLoader
                    {
                        ApiBaseUrl = ServerTxt.Text,
                        ApiBasePort = (int)ClientServerPortNum.Value,
                        AppDataPath = appDataPath,
                        ApiUrlVerb = "http"
                    };
                    Notify("Creating Client App configuration file ...");
                    binPath = Path.Combine(binPath, "AppSettings.json");
                    jsonContent = JsonSerializer.Serialize<ClientAppSettingLoader>(settings);
                    File.WriteAllText(binPath, jsonContent);
                    Notify($"Write config: {Environment.NewLine}{jsonContent}{Environment.NewLine} to {binPath}");

                    Notify("Creating Intrman SignalR Windows Service ...");
                    svcName = "IntrMan Signalr Service";
                    binPath = Path.Combine(path, "SignalR Service", "IntrManApp.SignalR.exe");
                    pi = new ProcessStartInfo("sc.exe",
                                $"create \"{svcName}\" binPath=\"{binPath}\" start=auto");
                    pi.CreateNoWindow = true;
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Registering Intrman SignalR Windows Service");
                    pi = new ProcessStartInfo("sc.exe", $"description \"{svcName}\" \"Intrepid SignalR service\"");
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Configuring Intrman SignalR Windows Service recovery options");
                    pi = new ProcessStartInfo("sc.exe", $"failure  \"{svcName}\" reset=0 actions=restart/10000/restart/10000/run/1000");
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Starting Intrman SignalR windows service");
                    pi = new ProcessStartInfo("sc.exe", $"start \"{svcName}\"");
                    Process.Start(pi);

                    binPath = Path.Combine(path, "WinBridge", "IntrManApp.WinBridge.exe");
                    Notify("Starting up Intrepid Win Bridge");
                    pi = new ProcessStartInfo(binPath)
                    {
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    Process.Start(pi);

                    var shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Manufacture");
                    Notify($"Creating Gha2z ERP shortcut: {shortcutPath} <==> " +
                        $"{Path.Combine(appDataPath, "Client App", "Gha2z ERP.exe")}");
                    var shortcut = File.CreateSymbolicLink(shortcutPath, Path.Combine(appDataPath, "Client App", "Gha2z ERP.exe"));
                    Notify(shortcut.FullName + " created successfully");
                    
                }
                else
                {
                    //Kill Gha2z ERP
                    var procs = Process.GetProcessesByName("Gha2z ERP");
                    if (procs.Length > 0)
                    {
                        Notify("Killing IntraManApp.WinBridge ...");
                        procs[0].Kill();
                    }

                    //Kill IntrManApp.WinBridge
                    procs = Process.GetProcessesByName("IntrManApp.WinBridge");
                    if (procs.Length > 0)
                    {
                        Notify("Killing IntraManApp.WinBridge ...");
                        procs[0].Kill();
                    }


                    pi = new ProcessStartInfo("sc.exe", $"stop \"{svcName}\"");
                    pi.CreateNoWindow = true;
                    Notify("Stopping Intrman Backend Windows Service ...");
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Deleting Intrman Backend Windows Service");
                    pi = new ProcessStartInfo("sc.exe", $"delete \"{svcName}\"");
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Stopping Intrman SignalR Windows Service ...");
                    svcName = "IntrMan Signalr Service";
                    pi = new ProcessStartInfo("sc.exe", $"stop \"{svcName}\"");
                    pi.CreateNoWindow = true;
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Notify("Deleting Intrman SignalR Windows Service");
                    pi = new ProcessStartInfo("sc.exe", $"delete \"{svcName}\"");
                    proc = Process.Start(pi);
                    proc?.WaitForExit();

                    Directory.Delete(appDataPath, true);

                    binPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Manufacture");
                    if (File.Exists(binPath))
                    {
                        Notify("Deleting Gha2z ERP shortcut ...");
                        File.Delete(binPath);
                    }
                }
                Notify("Configuration completed successfully");
                Thread.Sleep(3000);
                isFinished = true;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            TestServerBtn_Click(sender, e);
            if (sqlServerPassed && clientServerPassed)
            {
                pictureBox3.Visible = true;
                ContentPanel.Enabled = false;
                ContinueBtn.Enabled = false;
                TestServerBtn.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Configuration verification must be passed to continue ", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}

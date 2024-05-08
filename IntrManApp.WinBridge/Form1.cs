using IntrManApp.SignalRClient;
using Seagull.BarTender.Print;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace IntrManApp.WinBridge
{
    public partial class Form1 : Form
    {
        HubConnector hubConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            hubConnection = new HubConnector();
            hubConnection.StartingConnection += HubConnection_StartingConnection;
            hubConnection.ReceiveMessage += HubConnection_ReceiveMessage;
            hubConnection.EventMessage += HubConnection_EventMessage;

            try
            {
                await hubConnection.Start();
            }
            catch (Exception ex)
            {
                textBox1.AppendText($"{Environment.NewLine}{ex.Message}");
            }
            this.ResumeLayout();
        }

        private void HubConnection_EventMessage(object sender, string e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                textBox1.AppendText($"{Environment.NewLine}{e}");
            }));
        }

        private async void HubConnection_ReceiveMessage(object sender, HubMessageEventArgs e)
        {
            bool isAppMessage = e.Message.Contains("MessageType");
            this.Invoke(new MethodInvoker(delegate () 
            {
                textBox1.AppendText($"{Environment.NewLine}{sender}: {e.Message}, IsAppMessage:{isAppMessage}");
            }));
            if (!isAppMessage) return;
            try
            {
               
                var jsonMessage = JsonSerializer.Deserialize<AppMessage>(e.Message);
                string response = string.Empty;
                string resultString = string.Empty;
                switch (jsonMessage?.MessageType)
                {
                    case AppMessageType.GetInstalledPrinterList:
                        response = JsonSerializer.Serialize(
                            new AppMessage(AppMessageType.InstalledPrinterList, PrinterSettings.InstalledPrinters.Cast<string>().ToList()));
                        await hubConnection.SendMessage("Wintools", response);
                        break;
                    case AppMessageType.PrintLabel:
                        PrintArguments arguments = JsonSerializer.Deserialize<PrintArguments>(jsonMessage.Data.ToString());
                        string appName = "Intrepid ERP Label Print";
                        Engine engine = new Engine(true);
                        lock (engine)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                try
                                {
                               
                                        textBox1.Clear();
                                        this.Visible = true;
                                    this.Show();
                                    this.BringToFront();
                                        textBox1.Text = $"Processing label printing \"{arguments.LabelFilePath}\"";
                                
                                        var labelFormat = engine.Documents.Open(arguments.LabelFilePath);
                                        labelFormat.PrintSetup.PrinterName = arguments.PrinterName;
                                        labelFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
                                        labelFormat.PrintSetup.NumberOfSerializedLabels = 1;

                                        Messages btMessages;
                                        Result result = labelFormat.Print(appName, 10000, out btMessages);
                                        string btMessageString = string.Empty;

                                        foreach (Seagull.BarTender.Print.Message msg in btMessages)
                                        {
                                            btMessageString += "\n\n" + msg.Text;
                                        }

                                    
                                        if (result == Result.Failure)
                                            resultString = "Print Failed" + btMessageString;
                                        else
                                            resultString = "Label was successfully sent to printer." + btMessageString;
                                        textBox1.AppendText($"\n\nReceiving Bartender result messages:\n{result}\n\n{resultString}\n\nClosing label file ...");
                                        labelFormat.Close(SaveOptions.DoNotSaveChanges);
                              
                                }
                                catch (Exception ex)
                                {
                                    textBox1.AppendText($"{Environment.NewLine}{ex.Message}");
                                }
                                finally
                                {
                                    engine.Stop();
                                }
                                this.Hide();
                            }));
                        }
                        response = JsonSerializer.Serialize(
                          new AppMessage(AppMessageType.PrintLabelResult, resultString));
                        await hubConnection.SendMessage("Wintools", response);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    textBox1.AppendText($"{Environment.NewLine}{ex.Message}");
                }));
            }

        }

        private void HubConnection_StartingConnection(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                textBox1.AppendText($"Connecting to HubServer");
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                return;
            }

        }
    }
}

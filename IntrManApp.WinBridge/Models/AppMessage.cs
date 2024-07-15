using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.WinBridge
{
    public class AppMessage
    {
        public AppMessageType MessageType { get; set; } 
        public Object Data { get; set; } 

        public AppMessage(AppMessageType messageType, Object data)
        {
            this.MessageType = messageType;
            this.Data = data;
        }
    }

    public class PrintArguments
    {
        public string LabelFilePath { get; set; }
        public string PrinterName { get; set; }
        public PrintArguments(string labelFilePath, string printerName)
        {
            LabelFilePath = labelFilePath;
            PrinterName = printerName;
        }
    }

    public enum AppMessageType
    {
        GetInstalledPrinterList = 1,
        InstalledPrinterList = 2,
        PrintLabel = 3,
        PrintLabelResult = 4,
        GetFolderPath = 5,
        UserSelectedFolderPath = 6,
        GetFilePath = 7,
        UserSelectedFilePath = 8
    }
}

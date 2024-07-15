using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract.Tools
{
    public class AppMessage(AppMessageType messageType, Object? data)
    {
        public AppMessageType MessageType { get; set; } = messageType;
        public Object? Data { get; set; } = data;
    }

    public class PrintArguments(string labelFilePath,  string printerName)
    {
        public string LabelFilePath { get; set; } = labelFilePath;
        public string PrinterName { get; set; } = printerName;
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

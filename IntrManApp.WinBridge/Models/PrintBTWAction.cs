using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.WinBridge
{
    public class PrintBTWAction
    {
        public  string DocumentFile { get; set; }
        public string Printer { get; set; }
        public bool SaveAfterPrint { get; set; } = false;

    }

    public class BTAction
    {
        public  PrintBTWAction PrintBTWAction { get; set; }
    }
}

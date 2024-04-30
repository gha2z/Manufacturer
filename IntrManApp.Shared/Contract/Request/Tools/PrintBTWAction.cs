using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManAppHybridApp.Tools.Bartender
{
    public class PrintBTWAction
    {
        public required string DocumentFile { get; set; }
        public string? Printer { get; set; }
        public bool SaveAfterPrint { get; set; } = false;

    }

    public class BTAction
    {
        public required PrintBTWAction PrintBTWAction { get; set; }
    }
}

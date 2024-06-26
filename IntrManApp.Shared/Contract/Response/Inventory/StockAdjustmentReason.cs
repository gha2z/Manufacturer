using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class StockAdjustmentReason
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}

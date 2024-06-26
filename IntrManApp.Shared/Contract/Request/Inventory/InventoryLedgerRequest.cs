using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class InventoryLedgerRequest
    {
        public Guid ProductId { get; set; } = Guid.Empty;
        public Guid LocationId { get; set; } = Guid.Empty;
    }
}

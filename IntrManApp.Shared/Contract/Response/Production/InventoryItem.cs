using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class InventoryItem
    {
        public Guid CategoryId { get; set; } = Guid.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public Guid ProductId { get; set; } = Guid.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public string MeasurementUnitName { get; set; } = string.Empty;
        public Guid LocationId { get; set; } = Guid.Empty;
        public string Location { get; set; } = string.Empty;
        public string ColRow { get; set; } = string.Empty;

        public byte Flag { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal QtyAvailable { get; set; }
        public decimal QtyOnHand { get; set; }
        public IEnumerable<InventoryLedger> InventoryLedgers { get; set; } = [];
    }

    public class InventoryItemExtendedFlag
    {
        public Guid CategoryId { get; set; } = Guid.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public Guid ProductId { get; set; } = Guid.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public string MeasurementUnitName { get; set; } = string.Empty;
        public Guid LocationId { get; set; } = Guid.Empty;
        public string Location { get; set; } = string.Empty;
        public string ColRow { get; set; } = string.Empty;
        public decimal QtyAvailable { get; set; }
        public decimal QtyOnHand { get; set; }
        public IEnumerable<InventoryLedger> InventoryLedgers { get; set; } = [];
    }

    public class InventoryLedger
    {
        public DateTime TransDate { get; set; }
        public Guid InventoryId { get; set; } = Guid.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StockIn { get; set; }
        public decimal StockOut { get; set; }
        public decimal Balance { get; set; }
        public byte Flag { get; set; }
        public string Location { get; set; } = string.Empty;
        public string ColRow { get; set; } = string.Empty;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class InventoryItemDetail
    {
        public Guid ProductId { get; set; } = Guid.Empty;
        public Guid InventoryId { get; set; } = Guid.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public Guid MeasurementUnitId { get; set; } = Guid.Empty;
        public string MeasurementUnitName { get; set; } = string.Empty;
        public Guid LocationId { get; set; } = Guid.Empty;
        public string LocationName { get; set; } = string.Empty;
        public Guid RackingPalletId { get; set; } = Guid.Empty;
        public string RackingPalletColRow { get; set; } = string.Empty;
        public int DaysToExpire { get; set; }
        public int DaysToManufacture { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ProductionDate { get; set; }
        public Guid OutLocationId { get; set; } = Guid.Empty;
        public string OutLocationName { get; set; } = string.Empty;
        public Guid OutRackingPalletId { get; set; } = Guid.Empty;
        public string OutRackingPalletColRow { get; set; } = string.Empty;
        public Guid CurrentLocationId { get; set; } = Guid.Empty;
        public string CurrentLocationName { get; set; } = string.Empty;
        public Guid CurrentRackingPalletId { get; set; } = Guid.Empty;
        public string CurrentRackingPalletColRow { get; set; } = string.Empty;
        public bool IsFinishedGood { get; set; }

    }
}

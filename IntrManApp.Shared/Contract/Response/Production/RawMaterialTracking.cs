using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class RawMaterialTrackingResponse
    {
        public Guid RawMaterialId { get; set; } = Guid.Empty;
        public Guid InventoryId { get; set; } = Guid.Empty;
        public string CartonId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public Guid MeasurementUnitId { get; set; } = Guid.Empty;
        public string MeasurementUnitName { get; set; } = string.Empty;
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid LocationId { get; set; } = Guid.Empty;
        public string LocationName { get; set; } = string.Empty;
        public Guid RackingPalletId { get; set; } = Guid.Empty;
        public string ColRow { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public Guid SupplierId { get; set; } = Guid.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public decimal InitialWeight { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal AllocatedForProductionWeight { get; set; }
        public DateTime UsedInItemProductionDate { get; set; }

        public Guid EndProductId { get; set; } = Guid.Empty;
        public string EndProductBatchNumber { get; set; } = string.Empty;
        public string EndProductName { get; set; } = string.Empty;
        public decimal EndProductWeight { get; set; }
        public string EndProductMeasurementUnitName { get; set; } = string.Empty;
        public DateTime ReturnDate { get; set; }
        public decimal ReturnWeight { get; set; }

        public IEnumerable<InventoryLedger> InventoryLedgers { get; set; } = [];

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class ProductCheckOutRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime? CheckoutDate { get; set; }
        public List<ProductCheckOutDetailRequest> ProductCheckoutDetail { get; set; } = [];
    }

    public class ProductCheckOutDetailRequest
    {
        public string BatchNumber { get; set; } = string.Empty;
        public Guid InventoryId { get; set; }
        public Guid UnitMeasurementId { get; set; }
        public decimal Quantity { get; set; }
        public Guid LocationId { get; set; }
        public Guid RackingPalletId { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal InitialQuantity { get; set; }
        public string QuantityChangeReason { get; set; } = string.Empty;
        public InventoryItemDetail RawMaterial { get; set; } = new InventoryItemDetail();
        public LocationResponse? Location { get; set; }
        public RackingPalletResponse? RackingPallet { get; set; }
    }

    public class EndProductCheckOutRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime? CheckoutDate { get; set; }
        public List<EndProductCheckOutDetailRequest> ProductCheckoutDetail { get; set; } = [];
    }

    public class EndProductCheckOutDetailRequest
    {
        public string BatchNumber { get; set; } = string.Empty;
        public Guid InventoryId { get; set; }
        public Guid UnitMeasurementId { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public Guid LocationId { get; set; }
        public Guid RackingPalletId { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal InitialQuantity { get; set; }
        public string QuantityChangeReason { get; set; } = string.Empty;
        public EndProductItemDetail ItemDetail { get; set; } = new();
        public LocationResponse? Location { get; set; }
        public RackingPalletResponse? RackingPallet { get; set; }

    }
}

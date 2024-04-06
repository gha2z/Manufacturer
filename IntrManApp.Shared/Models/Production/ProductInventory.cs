using System;
using System.Collections.Generic;
using IntrManApp.Shared.Models.Sales;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductInventory
{
    public Guid InventoryId { get; set; }

    public Guid? ProductId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public Guid? LocationId { get; set; }

    public string? RackingPalletCol { get; set; }

    public short? RackingPalletRow { get; set; }

    public decimal Quantity { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    /// <summary>
    /// 1. CheckIn (purchase) 2. CheckOut for Production 3. Return from Production 4. Waiting for Production 5. In production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched
    /// </summary>
    public byte? Flag { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

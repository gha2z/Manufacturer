using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInventory
{
    public Guid InventoryId { get; set; }

    public Guid? ProductId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public Guid? LocationId { get; set; }

    public Guid? RackingPalletId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// 1. CheckIn (purchase) 2. CheckOut for Production 3. Return from Production 4. Waiting for Production 5. In production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched 11. Move location
    /// </summary>
    public byte? Flag { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? TransIdReference { get; set; }

    public decimal? TotalBatches { get; set; }

    public virtual InventoryFlag? FlagNavigation { get; set; }

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLines { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual RackingPallet? RackingPallet { get; set; }

    public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

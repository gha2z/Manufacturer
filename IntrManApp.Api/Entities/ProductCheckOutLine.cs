using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductCheckOutLine
{
    public Guid CheckOutId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Quantity { get; set; }

    public Guid? LocationId { get; set; }

    public Guid? RackingPalletId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? SourceLocationId { get; set; }

    public Guid? SourceRackingPalletId { get; set; }

    public virtual ProductCheckout CheckOut { get; set; } = null!;

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual RackingPallet? RackingPallet { get; set; }

    public virtual Location? SourceLocation { get; set; }

    public virtual RackingPallet? SourceRackingPallet { get; set; }
}

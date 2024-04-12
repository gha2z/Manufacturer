using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInternalCheckOutLine
{
    public Guid CheckOutId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Quantity { get; set; }

    public Guid? LocationId { get; set; }

    public string? RackingPalletCol { get; set; }

    public short? RackingPalletRow { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ProductInternalCheckout CheckOut { get; set; } = null!;

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }
}

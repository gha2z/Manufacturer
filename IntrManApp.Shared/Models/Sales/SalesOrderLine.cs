using System;
using System.Collections.Generic;
using IntrManApp.Shared.Models.Production;

namespace IntrManApp.Shared.Models.Sales;

public partial class SalesOrderLine
{
    public Guid OrderId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Quantity { get; set; }

    public Guid? LocationId { get; set; }

    public string? RackingPalletCol { get; set; }

    public short? RackingPalletRow { get; set; }

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual SalesOrder Order { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class SalesOrderLine
{
    public Guid OrderId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Quantity { get; set; }

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual SalesOrder Order { get; set; } = null!;
}

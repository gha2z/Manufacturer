using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductionOrderLineDetailResourceAllocation
{
    public Guid? ResourceId { get; set; }

    public Guid? ProductCheckoutId { get; set; }

    public Guid? InventoryId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public DateTime ModifierDate { get; set; }

    public virtual ProductInventory? Inventory { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual ProductionOrderLineDetailResource? Resource { get; set; }
}

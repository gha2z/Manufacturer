using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductionOrderLineDetailResourceAllocation
{
    public Guid? ResourceId { get; set; }

    public Guid? ProductCheckoutId { get; set; }

    public Guid? RawMaterialId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public DateTime ModifierDate { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual ProductInventory? RawMaterial { get; set; }

    public virtual ProductionOrderLineDetailResource? Resource { get; set; }
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductionOrderLineDetailResource
{
    public Guid? InventoryId { get; set; }

    public Guid? RawMaterialId { get; set; }

    public Guid ResourceId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public DateTime ModifierDate { get; set; }

    public virtual ProductionOrderLineDetail? Inventory { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? RawMaterial { get; set; }
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class MeasurementUnit
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? ChildId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? GroupId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<BillOfMaterial> BillOfMaterials { get; set; } = new List<BillOfMaterial>();

    public virtual MeasurementUnit? Child { get; set; }

    public virtual MeasurementUnitGroup? Group { get; set; }

    public virtual ICollection<MeasurementUnit> InverseChild { get; set; } = new List<MeasurementUnit>();

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLines { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<ProductionOrderLineDetailResource> ProductionOrderLineDetailResources { get; set; } = new List<ProductionOrderLineDetailResource>();

    public virtual ICollection<ProductionOrderLine> ProductionOrderLines { get; set; } = new List<ProductionOrderLine>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

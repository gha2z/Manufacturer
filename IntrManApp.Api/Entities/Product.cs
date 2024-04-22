using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid? CategoryId { get; set; }

    public string ProductNumber { get; set; } = null!;

    public bool IsFinishedGood { get; set; }

    public bool? IsSalable { get; set; }

    public bool? IsUniqueBatchPerOrder { get; set; }

    public decimal? SafetyStockLevel { get; set; }

    public decimal? ReorderPoint { get; set; }

    public decimal? StandardCost { get; set; }

    public decimal? ListPrice { get; set; }

    public Guid MeasurementUnitGroupId { get; set; }

    public Guid MeasurementUnitOrderId { get; set; }

    public decimal? OrderQuantity { get; set; }

    public int? DaysToManufacture { get; set; }

    public int? DaysToExpire { get; set; }

    public Guid LocationId { get; set; }

    public Guid? RackingPalletId { get; set; }

    public string? AdditionalInfo { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<BillOfMaterial> BillOfMaterialProducts { get; set; } = new List<BillOfMaterial>();

    public virtual ICollection<BillOfMaterial> BillOfMaterialRawMaterials { get; set; } = new List<BillOfMaterial>();

    public virtual ProductCategory? Category { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual MeasurementUnitGroup MeasurementUnitGroup { get; set; } = null!;

    public virtual MeasurementUnit MeasurementUnitOrder { get; set; } = null!;

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<ProductNameAndDescriptionCulture> ProductNameAndDescriptionCultures { get; set; } = new List<ProductNameAndDescriptionCulture>();

    public virtual ICollection<ProductionOrderLineDetailResource> ProductionOrderLineDetailResources { get; set; } = new List<ProductionOrderLineDetailResource>();

    public virtual ICollection<ProductionOrderLine> ProductionOrderLines { get; set; } = new List<ProductionOrderLine>();

    public virtual RackingPallet? RackingPallet { get; set; }
}

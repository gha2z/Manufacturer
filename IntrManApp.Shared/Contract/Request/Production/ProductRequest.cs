namespace IntrManApp.Shared.Contract;

public class ProductNameRequest
{
    public string CultureId { get; set; } = "en-US";
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}

public class MeasurementUnitGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class MeasurementUnitRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? ChildId { get; set; }

    public decimal Quantity { get; set; }

    public Guid? GroupId { get; set; }
    public string Initial { get; set; } = string.Empty;
}

    public class ProductVariantRequest
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid ProductId { get; set; }
    public decimal Weight { get; set; } = 1;
    public Guid MeasurementUnitId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal StandardCost { get; set; } = 0;
    public decimal ListPrice { get; set; } = 0;
    public string Caption { get; set; } = string.Empty;
    public virtual MeasurementUnitRequest MeasurementUnit { get; set; } = null!;

    public override string ToString()
    {
        return string.IsNullOrEmpty(Caption) ? $"{Weight:N2} {MeasurementUnit.Initial}" : Caption;
    }

}

    public class ProductRequest
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; } = Guid.Empty;

    public string ProductNumber { get; set; } = null!;

    public virtual ICollection<ProductNameRequest> ProductNameAndDescriptionCultures { get; set; } = [];

    public bool IsFinishedGood { get; set; }

    public bool? IsSalable { get; set; }

    public bool? IsUniqueBatchPerOrder { get; set; }

    public decimal? SafetyStockLevel { get; set; }

    public decimal? ReorderPoint { get; set; }

    public decimal? StandardCost { get; set; }

    public decimal? ListPrice { get; set; }

    public Guid MeasurementUnitGroupId { get; set; } = Guid.Empty;

    public Guid MeasurementUnitOrderId { get; set; } = Guid.Empty;

    public decimal? OrderQuantity { get; set; }

    public int? DaysToManufacture { get; set; }

    public int? DaysToExpire { get; set; }

    public Guid LocationId { get; set; } = Guid.Empty;

    public Guid RackingPalletId { get; set; } = Guid.Empty;
    public Guid OutLocationId { get; set; } = Guid.Empty;
    public Guid OutRackingPalletId { get; set; } = Guid.Empty;

    public string? AdditionalInfo { get; set; }

    public virtual ICollection<ProductVariantRequest> ProductVariants { get; set; } = new List<ProductVariantRequest>();

    public string Names => string.Join("/", ProductNameAndDescriptionCultures.Select(x => x.Name));
}

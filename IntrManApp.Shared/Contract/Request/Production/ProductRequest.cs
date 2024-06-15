namespace IntrManApp.Shared.Contract;

public class ProductNameRequest
{
    public string CultureId { get; set; } = "en-US";
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
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
}

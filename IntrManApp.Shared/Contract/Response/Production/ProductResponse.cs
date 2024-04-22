using System.Text.Json.Serialization;

namespace IntrManApp.Shared.Contract;

public class ProductResponse
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } 

    public Guid Id { get; set; }
    public string Names { get; set; } = string.Empty;
    public string ProductNumber { get; set; } = null!;

    public bool IsFinishedGood { get; set; }

    public bool? IsSalable { get; set; }

    public bool? IsUniqueBatchPerOrder { get; set; }

    public decimal? SafetyStockLevel { get; set; }

    public decimal? ReorderPoint { get; set; }

    public decimal? StandardCost { get; set; }

    public decimal? ListPrice { get; set; }

    public Guid MeasurementUnitGroupId { get; set; }
    public string MeasurementUnitGroupName { get; set; }

    public Guid MeasurementUnitOrderId { get; set; } 
    public string measurementUnitOrderName { get; set; }
    public decimal? OrderQuantity { get; set; }

    public int? DaysToManufacture { get; set; }

    public int? DaysToExpire { get; set; }

    public Guid LocationId { get; set; } 
    public string LocationName { get; set; }

    public Guid RackingPalletId { get; set; } 
    public string RackingPalletName { get; set; }
    public string RackingPalletCol { get; set; }
    public short RackingPalletRow { get; set; }
    public string? AdditionalInfo { get; set; }
    public short BomCount { get; set; }
    public ICollection<BomSpecificationResponse> Boms { get; set; } = [];
}

[JsonSerializable(typeof(List<ProductResponse>))]

public sealed partial class ProductContext: JsonSerializerContext
{
}

public class ProductNameResponse
{
    public string CultureId { get; set; } = "en-US";
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductVariant
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public decimal Weight { get; set; }

    public Guid MeasurementUnitId { get; set; }

    public string? Sku { get; set; }

    public decimal? StandardCost { get; set; }

    public decimal? ListPrice { get; set; }

    public string? Caption { get; set; }

    public virtual MeasurementUnit MeasurementUnit { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

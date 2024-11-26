using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class PurchaseRequestLine
{
    public Guid? RequestId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Quantity { get; set; }

    public Guid? ProductVariantId { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductVariant? ProductVariant { get; set; }

    public virtual PurchaseRequest? Request { get; set; }
}

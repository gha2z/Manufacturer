using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class BillOfMaterial
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid RawMaterialId { get; set; }

    public Guid RawMaterialMeasurementUnitId { get; set; }

    public decimal RawMaterialQuantity { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Product RawMaterial { get; set; } = null!;

    public virtual MeasurementUnit RawMaterialMeasurementUnit { get; set; } = null!;
}

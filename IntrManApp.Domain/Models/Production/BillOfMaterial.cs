using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class BillOfMaterial
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? RawMaterialId { get; set; }

    public Guid? RawMaterialMeasurementUnitId { get; set; }

    public decimal? RawMaterialQuantity { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Product? RawMaterial { get; set; }

    public virtual MeasurementUnit? RawMaterialMeasurementUnit { get; set; }
}

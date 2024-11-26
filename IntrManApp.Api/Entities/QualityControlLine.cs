using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class QualityControlLine
{
    public Guid? QcId { get; set; }

    public Guid? LineId { get; set; }

    public Guid? QcParamId { get; set; }

    public byte[]? Value { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Quantity { get; set; }

    public virtual ProductInventory? Line { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual QualityControl? Qc { get; set; }

    public virtual QualityControlParam? QcParam { get; set; }
}

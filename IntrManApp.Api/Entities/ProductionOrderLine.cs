﻿using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductionOrderLine
{
    public Guid LineId { get; set; }

    public Guid? ProductionOrderId { get; set; }

    public Guid? ProductId { get; set; }

    public decimal TotalBatches { get; set; }

    public decimal QuantityPerBatch { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? TotalBatchesCompleted { get; set; }

    public decimal? TotalBatchesScrapped { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductionOrder? ProductionOrder { get; set; }

    public virtual ICollection<ProductionOrderLineDetail> ProductionOrderLineDetails { get; set; } = new List<ProductionOrderLineDetail>();
}

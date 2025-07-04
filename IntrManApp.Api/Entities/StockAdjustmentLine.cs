﻿using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class StockAdjustmentLine
{
    public Guid AdjustmentId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? InitialQuantity { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Adjustment { get; set; }

    public Guid? ReasonId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? ProductionDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public Guid? LocationId { get; set; }

    public Guid? RackingPalletId { get; set; }

    public virtual StockAdjustMent AdjustmentNavigation { get; set; } = null!;

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual DiscrepantReason? Reason { get; set; }
}

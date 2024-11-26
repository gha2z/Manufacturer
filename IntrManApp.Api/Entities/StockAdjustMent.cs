using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class StockAdjustMent
{
    public Guid Id { get; set; }

    public DateTime? AdjustmentDate { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public bool? FromInventoryTransfer { get; set; }

    public virtual ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

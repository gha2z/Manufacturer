using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class DiscrepantReason
{
    public Guid Id { get; set; }

    public string Reason { get; set; } = null!;

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

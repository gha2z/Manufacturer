using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class StockAdjustMent
{
    public Guid Id { get; set; }

    public DateTime? AdjustmentDate { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public virtual ICollection<StockAdjustmentLine> StockAdjustmentLines { get; set; } = new List<StockAdjustmentLine>();
}

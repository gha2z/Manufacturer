using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class QualityControl
{
    public Guid Id { get; set; }

    public DateTime? CheckDate { get; set; }

    public Guid? ProductCheckInRefId { get; set; }

    public Guid? StockAdjustmentRefId { get; set; }

    public virtual ProductCheckIn? ProductCheckInRef { get; set; }

    public virtual StockAdjustMent? StockAdjustmentRef { get; set; }
}

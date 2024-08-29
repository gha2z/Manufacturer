using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductCheckInLine
{
    public Guid LineId { get; set; }

    public Guid? CheckInId { get; set; }

    public short? LineIndex { get; set; }

    public Guid? ProductId { get; set; }

    public int TotalBatches { get; set; }

    public decimal QuantityPerBatch { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public DateTime ProductionDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public Guid? LocationId { get; set; }

    public Guid? RackingPalletId { get; set; }

    public virtual ProductCheckIn? CheckIn { get; set; }

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ICollection<ProductCheckInLineDetail> ProductCheckInLineDetails { get; set; } = new List<ProductCheckInLineDetail>();

    public virtual RackingPallet? RackingPallet { get; set; }
}

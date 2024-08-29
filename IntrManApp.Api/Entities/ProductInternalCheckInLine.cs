using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInternalCheckInLine
{
    public Guid CheckInId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Quantity { get; set; }

    /// <summary>
    /// Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    public Guid LineId { get; set; }

    public virtual ProductInternalCheckIn CheckIn { get; set; } = null!;

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual ICollection<ProductInternalCheckInLinePackaging> ProductInternalCheckInLinePackagings { get; set; } = new List<ProductInternalCheckInLinePackaging>();
}

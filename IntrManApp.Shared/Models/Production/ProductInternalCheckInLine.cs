using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductInternalCheckInLine
{
    public Guid CheckInId { get; set; }

    public Guid InventoryId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Quantity { get; set; }

    public Guid? LocationId { get; set; }

    public string? RackingPalletCol { get; set; }

    public short? RackingPalletRow { get; set; }

    public byte? CheckinType { get; set; }

    /// <summary>
    /// Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ProductInternalCheckIn CheckIn { get; set; } = null!;

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual Location? Location { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }
}

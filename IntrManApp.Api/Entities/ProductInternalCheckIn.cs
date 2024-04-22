using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInternalCheckIn
{
    public Guid Id { get; set; }

    public DateTime? CheckInDate { get; set; }

    public byte? CheckInType { get; set; }

    /// <summary>
    /// 3. Return from Production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched
    /// </summary>
    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public virtual InventoryFlag? CheckInTypeNavigation { get; set; }

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();
}

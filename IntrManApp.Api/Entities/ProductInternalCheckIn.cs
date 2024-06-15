using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInternalCheckIn
{
    public Guid Id { get; set; }

    public DateTime? CheckInDate { get; set; }

    public byte? CheckInType { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public virtual InventoryFlag? CheckInTypeNavigation { get; set; }

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();
}

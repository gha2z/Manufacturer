using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductInternalCheckIn
{
    public Guid Id { get; set; }

    public DateTime? CheckInDate { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime ModifierDate { get; set; }

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();
}

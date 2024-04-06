using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Purchasing;

public partial class ProductCheckIn
{
    public Guid Id { get; set; }

    public DateTime? CheckInDate { get; set; }

    public Guid? SupplierId { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual Supplier? Supplier { get; set; }
}

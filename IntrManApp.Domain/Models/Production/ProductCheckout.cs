using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductCheckout
{
    public Guid Id { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLines { get; set; } = new List<ProductCheckOutLine>();
}

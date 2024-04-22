using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductInternalCheckout
{
    public Guid Id { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public byte? CheckOutType { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifierDate { get; set; }

    public virtual InventoryFlag? CheckOutTypeNavigation { get; set; }

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; } = new List<ProductInternalCheckOutLine>();
}

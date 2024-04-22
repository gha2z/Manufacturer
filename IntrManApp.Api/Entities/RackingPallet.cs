using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class RackingPallet
{
    public Guid Id { get; set; }

    public string? Col { get; set; }

    public short? Row { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLineRackingPallets { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLineSourceRackingPallets { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLineRackingPallets { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLineSourceRackingPallets { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLineRackingPallets { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLineSourceRackingPallets { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

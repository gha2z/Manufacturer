using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class InventoryFlag
{
    public byte Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ProductInternalCheckIn> ProductInternalCheckIns { get; set; } = new List<ProductInternalCheckIn>();

    public virtual ICollection<ProductInternalCheckout> ProductInternalCheckouts { get; set; } = new List<ProductInternalCheckout>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
}

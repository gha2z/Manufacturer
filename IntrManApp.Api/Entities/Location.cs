using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLineLocations { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLineSourceLocations { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductInternalCheckInLinePackaging> ProductInternalCheckInLinePackagingLocations { get; set; } = new List<ProductInternalCheckInLinePackaging>();

    public virtual ICollection<ProductInternalCheckInLinePackaging> ProductInternalCheckInLinePackagingSourceLocations { get; set; } = new List<ProductInternalCheckInLinePackaging>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLineLocations { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLineSourceLocations { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

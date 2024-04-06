using System;
using System.Collections.Generic;
using IntrManApp.Shared.Models.Purchasing;
using IntrManApp.Shared.Models.Sales;

namespace IntrManApp.Shared.Models.Production;

public partial class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();

    public virtual ICollection<ProductCheckOutLine> ProductCheckOutLines { get; set; } = new List<ProductCheckOutLine>();

    public virtual ICollection<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; } = new List<ProductInternalCheckInLine>();

    public virtual ICollection<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; } = new List<ProductInternalCheckOutLine>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();
}

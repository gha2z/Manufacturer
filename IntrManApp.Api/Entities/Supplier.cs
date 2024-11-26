using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Supplier
{
    public Guid BusinessEntityId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ICollection<ProductCheckIn> ProductCheckIns { get; set; } = new List<ProductCheckIn>();

    public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
}

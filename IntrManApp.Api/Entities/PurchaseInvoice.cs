using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class PurchaseInvoice
{
    public Guid Id { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public Guid? SupplierId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? PurchaseOrderId { get; set; }

    public virtual PurchaseOrder? PurchaseOrder { get; set; }

    public virtual Supplier? Supplier { get; set; }
}

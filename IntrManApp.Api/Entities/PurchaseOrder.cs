using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class PurchaseOrder
{
    public Guid Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public Guid? SupplierId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? PurchaseRequestId { get; set; }

    public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();

    public virtual PurchaseRequest? PurchaseRequest { get; set; }
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class PurchaseRequest
{
    public Guid Id { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}

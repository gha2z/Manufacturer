using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Customer
{
    public Guid BusinessEntityId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
}

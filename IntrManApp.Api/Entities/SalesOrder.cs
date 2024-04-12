using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class SalesOrder
{
    public Guid Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public Guid? CustomerId { get; set; }

    /// <summary>
    /// 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched
    /// </summary>
    public byte? Status { get; set; }

    public byte? RevisionNumber { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();
}

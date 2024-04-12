using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductionOrder
{
    public Guid Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public virtual ICollection<ProductionOrderLine> ProductionOrderLines { get; set; } = new List<ProductionOrderLine>();
}

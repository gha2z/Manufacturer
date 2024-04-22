using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductionOrderLineDetail
{
    public Guid? LineId { get; set; }

    public Guid InventoryId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public virtual ProductionOrderLine? Line { get; set; }
}

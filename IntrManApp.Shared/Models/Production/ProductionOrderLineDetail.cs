using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductionOrderLineDetail
{
    public Guid? LineId { get; set; }

    public Guid InventoryId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ProductionOrderLine? Line { get; set; }

    public virtual ICollection<ProductionOrderLineDetailResource> ProductionOrderLineDetailResources { get; set; } = new List<ProductionOrderLineDetailResource>();
}

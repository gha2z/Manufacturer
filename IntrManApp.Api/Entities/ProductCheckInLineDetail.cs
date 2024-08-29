using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductCheckInLineDetail
{
    public Guid InventoryId { get; set; }

    public Guid? LineId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public virtual ProductCheckInLine? Line { get; set; }
}

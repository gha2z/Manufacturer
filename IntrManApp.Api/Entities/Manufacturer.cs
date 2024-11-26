using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Manufacturer
{
    public Guid BusinessEntityId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ICollection<ProductCheckInLine> ProductCheckInLines { get; set; } = new List<ProductCheckInLine>();
}

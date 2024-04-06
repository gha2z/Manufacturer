using System;
using System.Collections.Generic;
using IntrManApp.Shared.Models.Person;

namespace IntrManApp.Shared.Models.Purchasing;

public partial class Supplier
{
    public Guid BusinessEntityId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ICollection<ProductCheckIn> ProductCheckIns { get; set; } = new List<ProductCheckIn>();
}

using System;
using System.Collections.Generic;
using IntrManApp.Shared.Models.Purchasing;
using IntrManApp.Shared.Models.Sales;

namespace IntrManApp.Shared.Models.Person;

public partial class BusinessEntity
{
    public Guid Id { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();

    public virtual Customer? Customer { get; set; }

    public virtual Person? Person { get; set; }

    public virtual Supplier? Supplier { get; set; }
}

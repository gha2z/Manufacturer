using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ContactType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();
}

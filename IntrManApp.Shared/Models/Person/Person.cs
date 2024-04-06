using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Person;

public partial class Person
{
    public Guid BusinessEntityId { get; set; }

    public Guid? PersonTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    public string? AdditionalContactInfo { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new List<BusinessEntityContact>();

    public virtual PersonType? PersonType { get; set; }
}

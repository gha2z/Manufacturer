using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Person;

public partial class BusinessEntityContact
{
    public Guid BusinessEntityId { get; set; }

    public Guid PersonId { get; set; }

    public Guid ContactTypeId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BusinessEntity BusinessEntity { get; set; } = null!;

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Person;

public partial class PersonType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}

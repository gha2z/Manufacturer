using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public Guid? TypeId { get; set; }

    public virtual UserType? Type { get; set; }
}

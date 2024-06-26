using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class UserType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserTypeFeature> UserTypeFeatures { get; set; } = new List<UserTypeFeature>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

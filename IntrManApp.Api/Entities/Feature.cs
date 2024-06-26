using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Feature
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid? ParentId { get; set; }

    public string? Path { get; set; }

    public string? Icon { get; set; }

    public virtual ICollection<UserTypeFeature> UserTypeFeatures { get; set; } = new List<UserTypeFeature>();
}

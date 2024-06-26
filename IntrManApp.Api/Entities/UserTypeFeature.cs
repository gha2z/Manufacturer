using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class UserTypeFeature
{
    public Guid FeatureId { get; set; }

    public Guid UserTypeId { get; set; }

    public bool? Accessible { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public virtual UserType UserType { get; set; } = null!;
}

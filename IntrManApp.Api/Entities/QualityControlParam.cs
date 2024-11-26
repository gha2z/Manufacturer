using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class QualityControlParam
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public byte[]? DefaultValue { get; set; }

    public byte? ValueType { get; set; }
}

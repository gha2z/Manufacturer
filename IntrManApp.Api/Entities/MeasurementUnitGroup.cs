using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class MeasurementUnitGroup
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<MeasurementUnit> MeasurementUnits { get; set; } = new List<MeasurementUnit>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

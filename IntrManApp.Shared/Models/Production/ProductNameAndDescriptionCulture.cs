using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductNameAndDescriptionCulture
{
    public Guid ProductId { get; set; }

    public string CultureId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Culture Culture { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class Culture
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ProductNameAndDescriptionCulture> ProductNameAndDescriptionCultures { get; set; } = new List<ProductNameAndDescriptionCulture>();
}

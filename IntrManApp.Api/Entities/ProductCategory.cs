using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class ProductCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

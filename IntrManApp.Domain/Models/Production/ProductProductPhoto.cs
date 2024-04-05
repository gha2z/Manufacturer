using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductProductPhoto
{
    public Guid? ProductId { get; set; }

    public Guid? ProductPhoto { get; set; }

    public bool Primary { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductPhoto? ProductPhotoNavigation { get; set; }
}

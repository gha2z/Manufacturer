using System;
using System.Collections.Generic;

namespace IntrManApp.Shared.Models.Production;

public partial class ProductPhoto
{
    public Guid Id { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    public string? ThumbnailPhotoFileName { get; set; }

    public byte[]? LargePhoto { get; set; }

    public string? LargePhotoFileName { get; set; }

    public DateTime ModifiedDate { get; set; }
}

using System;
using System.Collections.Generic;

namespace IntrManApp.Api.Entities;

public partial class PurchaseInvoiceLine
{
    public Guid? PurchaseInvoiceId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? DiscPrc { get; set; }

    public decimal? DiscNom { get; set; }

    public bool? IsDiscPrc { get; set; }

    public decimal? SubTotal { get; set; }

    public Guid? ProductVariantId { get; set; }

    public virtual MeasurementUnit? MeasurementUnit { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductVariant? ProductVariant { get; set; }

    public virtual PurchaseInvoice? PurchaseInvoice { get; set; }
}

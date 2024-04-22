using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class ProductCheckinRequest
{
    public Guid SupplierId { get; set; }
    public DateTime? CheckInDate { get; set; }
    public List<ProductCheckInLineRequest> ProductCheckInDetail { get; set; } = [];
}

public class UpdateProductCheckinRequest
{
    public Guid Id { get; set; }
    public Guid SupplierId { get; set; }
    public DateTime? CheckInDate { get; set; }
    public List<ProductCheckInLineRequest> ProductCheckInDetail { get; set; } = [];
}

public class ProductCheckInLineRequest
{
    public Guid LineId { get; set; }
    public Guid ProductId { get; set; }
    public int TotalBatches { get; set; }
    public decimal QuantityPerBatch { get; set; }
    public Guid UnitMeasurementId { get; set; }
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid LocationId { get; set; }
    public Guid RackingPalleteId { get; set; }

}

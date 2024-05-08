using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class ProductionOrderRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ScheduleDate { get; set; } = DateTime.Now;
        public List<ProductionOrderLineRequest> ProductionOrderLines { get; set; } = [];
    }

    public class ProductionOrderLineRequest
    {
        public Guid ProductId { get; set; }
        public int TotalBatches { get; set; }
        public decimal QuantityPerBatch { get; set; }
        public Guid MeasurementUnitId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public EndProduct? EndProduct { get; set; }
    }
}

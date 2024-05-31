using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
  
    public class DispatchRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime? DispatchDate { get; set; }
        public Guid CustomerId { get; set; }
        public List<DispatchLineRequest> DispatchLines { get; set; } = [];
    }

    public class DispatchLineRequest
    {
        public string BatchNumber { get; set; } = string.Empty;
        public Guid InventoryId { get; set; }
        public Guid MeasurementUnitId { get; set; }
        public decimal Quantity { get; set; }
        public InventoryItem? FinishedProduct { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class DispatchOrderDetail
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
	    public string? CustomerName { get; set; }
        public Guid ProductId { get; set; }
		public string? ProductName { get; set; }
        public string? BatchNumber { get; set; }
        public Guid InventoryId { get; set; }
        public decimal Weight { get; set; }
        public Guid MeasurementUnitId { get; set; }
	    public string? ProductMeasurementUnitName { get; set; }
        public decimal Quantity { get; set; }
        public byte Flag { get; set; }
    }
}

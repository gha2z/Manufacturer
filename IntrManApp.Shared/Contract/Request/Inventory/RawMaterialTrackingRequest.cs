using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class RawMaterialTrackingRequest
    {
        public Guid RawMaterialId { get; set; } = Guid.Empty;
        public string CartonId { get; set; } = string.Empty;
        public Guid SupplierId { get; set; } = Guid.Empty;
        public DateTime CheckInDate { get; set; } = DateTime.Now;
        public DateTime CheckOutDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; } = DateTime.Now;
        public Guid EndProductId { get; set; } = Guid.Empty;
        public string EndProductBatchNumber { get; set; } = string.Empty;
        public DateTime EndProductionStartDate { get; set; } = DateTime.Now;
    }
}

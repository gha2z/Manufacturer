using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class BomSpecificationResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid RawMaterialId { get; set; }
        public string RawMaterialName { get; set; } = string.Empty;
        public Guid RawMaterialMeasurementUnitId { get; set; }
        public string RawMaterialMeasurementUnitName { get; set; } = string.Empty;
        public decimal RawMaterialQuantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
    public  class BomResponse
    {
        
        public Guid ProductId { get; set; }
        public List<BomSpecificationResponse> BomSpecifications { get; set; } = [];
      
    }
}

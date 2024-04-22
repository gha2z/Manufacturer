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
        public string RawMaterialName { get; set; }
        public Guid RawMaterialMeasurementUnitId { get; set; }
        public string RawMaterialMeasurementUnitName { get; set; }
        public decimal RawMaterialQuantity { get; set; }
    }
    public  class BomResponse
    {
        
        public Guid ProductId { get; set; }
        public List<BomSpecificationResponse> BomSpecifications { get; set; } = [];
      
    }
}

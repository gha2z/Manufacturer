using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class BomSpecification
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid RawMaterialId { get; set; }
        public Guid RawMaterialMeasurementUnitId { get; set; }
        public decimal RowMaterialQuantity { get; set; }
    }
    public  class BomRequest
    {
        
        public Guid ProductId { get; set; }
        public List<BomSpecification> BomSpecifications { get; set; } = [];
      
    }
}

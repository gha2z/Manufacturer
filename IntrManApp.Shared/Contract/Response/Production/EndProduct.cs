using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class EndProduct
    {
        public Guid Id { get; set; }
        public string ProductNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid MeasurementUnitOrderId { get; set; }
        public string MeasurementUnitOrderName { get; set; } = string.Empty;
        public Guid MeasurementUnitGroupId { get; set; }
        public string MeasurementUnitGroupName { get; set; } = string.Empty;
        public Guid LocationId { get; set; } = Guid.Empty;
        public Guid RackingPalletId { get; set; } = Guid.Empty;
        public int DaysToExpire { get; set; }
        public int DaysToManufacture { get; set; }
        public Decimal OrderQuantity { get; set; }
    }
}

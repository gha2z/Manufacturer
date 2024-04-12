using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class ProductCheckOutRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime? CheckoutDate { get; set; }
        public List<ProductCheckOutDetailRequest> RowMaterialList { get; set; } = [];
    }

    public class ProductCheckOutDetailRequest
    {
        public Guid InventoryId { get; set; }
        public Guid UnitMeasurementId { get; set; }
        public decimal Quantity { get; set; }
        public Guid LocationId { get; set; }
        public string RackingPalletCol { get; set; } = string.Empty;
        public short RackingPalletRow { get; set; } = 0;

    }
}

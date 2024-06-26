using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class StockAdjustmentRequest
{
    public List<StockAdjustmentLineRequest> Items { get; set; } = [];
}

public class StockAdjustmentLineRequest
{
    public Guid InventoryId { get; set; }
    public decimal Quantity { get; set; }
    public Guid UnitMeasurementId { get; set; }
    public decimal InitialQuantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public InventoryItemDetail ItemDetail { get; set; } = new();
    public DateTime ExpirationDate { get; set; }
    public DateTime ProductionDate { get; set; }

}

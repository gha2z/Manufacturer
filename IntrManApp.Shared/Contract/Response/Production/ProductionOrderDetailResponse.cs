using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class ProductionOrderDetailResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ScheduleDate { get; set; } = DateTime.Now;
        public Guid LineId { get; set; } = Guid.Empty;
        public Guid ProductId { get; set; } = Guid.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public Guid ProductMeasurementUnitId { get; set; } = Guid.Empty;
        public string ProductMeasurementUnitName { get; set; } = string.Empty;
        public decimal QuantityPerBatch { get; set; }
        public int TotalBatches { get; set; }
        public Guid InventoryId { get; set; } = Guid.Empty;
        public Guid RawMaterialId { get; set; } = Guid.Empty;
        public string RawMaterialName { get; set; } = string.Empty;
        public Guid RawMaterialMeasurementUnitId { get; set; } = Guid.Empty;
        public string RawMaterialMeasurementUnitName { get; set; } = string.Empty;
        public decimal RawMaterialQuantity { get; set; }
        public byte Flag { get; set; }
        public float ResourceAllocated { get; set; }
    }

    public class ProductionItem
    {
        public Guid LineId { get; set; } = Guid.Empty;
        public Guid InventoryId { get; set; } = Guid.Empty;
        public Guid ProductId { get; set; } = Guid.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public Guid ProductMeasurementUnitId { get; set; } = Guid.Empty;
        public string ProductMeasurementUnitName { get; set; } = string.Empty;
        public decimal QuantityPerBatch { get; set; }
        public int TotalBatches { get; set; }
        public int BatchIndex { get; set; }
        public byte Flag { get; set; }
        public float ResourceAllocated { get; set; }
        public List<BomSpecificationResponse> BomItems { get; set; } = [];
        public string IngredientName_1 { get; set; } = string.Empty;
        public string IngredientName_2 { get; set; } = string.Empty;
        public string IngredientName_3 { get; set; } = string.Empty;
        public string IngredientName_4 { get; set; } = string.Empty;
        public string IngredientName_5 { get; set; } = string.Empty;
        public string IngredientName_6 { get; set; } = string.Empty;
        public string IngredientName_7 { get; set; } = string.Empty;
        public string IngredientName_8 { get; set; } = string.Empty;
        public string IngredientName_9 { get; set; } = string.Empty;
        public string IngredientQty_1 { get; set; } = string.Empty;

        public string IngredientQty_2 { get; set; } = string.Empty;
        public string IngredientQty_3 { get; set; } = string.Empty;
            
        public string IngredientQty_4 { get; set; } = string.Empty;
        public string IngredientQty_5 { get; set; } = string.Empty;
        public string IngredientQty_6 { get; set; } = string.Empty;
        public string IngredientQty_7 { get; set; } = string.Empty;
        public string IngredientQty_8 { get; set; } = string.Empty;
        public string IngredientQty_9 { get; set; } = string.Empty;

            
    }


}

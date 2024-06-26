using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItemDetail>> GetInventoryItemsByLocation();
        Task<Guid> CreateProductCheckout(ProductCheckOutRequest request);

        Task<IEnumerable<InventoryItemExtendedFlag>> GetFinishedProductInventoriesAsync();

        IEnumerable<InventoryLedger> GetInventoryLedger(InventoryLedgerRequest request);

        Task<IEnumerable<InventoryItem>> GetRawMaterialInventoriesAsync();

        IEnumerable<InventoryLedger> GetRawMaterialInventoryLedger(InventoryLedgerRequest request);

        Task<IEnumerable<RawMaterialTrackingResponse>> RawMaterialTracking(RawMaterialTrackingRequest request);

        IEnumerable<InventoryLedger> GetRawMaterialInventoryLedgerById(Guid id);
        Task<Guid> CreateStockAdjustment(StockAdjustmentRequest request);

        Task<IEnumerable<StockAdjustmentReason>> GetStockAdjustmentReasonsAsync();
    }
}

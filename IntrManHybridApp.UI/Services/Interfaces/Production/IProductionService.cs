using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services;

public interface IProductionService
{
    Task<Guid> CreateProductionOrderAsync(ProductionOrderRequest request);
    Task<bool> DeleteProductionOrderAsync(Guid id);
    Task<ProductionOrderRequest> GetProductionOrderAsync(Guid id);
    Task<IEnumerable<ProductionOrderRequest>> GetProductionOrdersAsync();
    Task<IEnumerable<EndProduct>> GetEndProductsAsync();
    Task<IEnumerable<ProductionItem>> GetProductionOrderItemsAsync(Guid id);
    Task<IEnumerable<ProductionItem>> GetProductionOrderItemsByDateAsync(DateTime date);
    Task<bool> StartItemProduction(Guid inventoryId);
    Task<bool> AbortItemProduction(Guid inventoryId);

    Task<IEnumerable<InventoryItemDetail>> GetRawMaterialsForProductionAsync();
    Task<Guid> CreateRawMaterialsCheckout(ProductCheckOutRequest request);

    Task<IEnumerable<InventoryItemDetail>> GetRunningProductionItemsAsync();
    Task<Guid> CreateFinishedProductCheckin(FinishedProductInternalCheckinRequest request);
    Task<IEnumerable<InventoryItemExtendedFlag>> GetFinishedProductInventoriesAsync();

    IEnumerable<InventoryLedger> GetInventoryLedger(InventoryLedgerRequest request);

    Task<IEnumerable<InventoryItem>> GetRawMaterialInventoriesAsync();

    IEnumerable<InventoryLedger> GetRawMaterialInventoryLedger(InventoryLedgerRequest request);

    Task<IEnumerable<RawMaterialTrackingResponse>> RawMaterialTracking(RawMaterialTrackingRequest request);

    IEnumerable<InventoryLedger> GetRawMaterialInventoryLedgerById(Guid id);

}

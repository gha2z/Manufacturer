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
    Task<IEnumerable<ProductionItem>> GetProductionOrderItemsByStatusAsync(int status);
    Task<bool> StartItemProduction(Guid inventoryId);
    Task<bool> AbortItemProduction(Guid inventoryId);

    Task<IEnumerable<InventoryItemDetail>> GetRawMaterialsForProductionAsync();
    Task<Guid> CreateRawMaterialsCheckout(ProductCheckOutRequest request);

    Task<IEnumerable<InventoryItemDetail>> GetRunningProductionItemsAsync();
    Task<Guid> CreateFinishedProductCheckin(FinishedProductInternalCheckinRequest request);

    Task<IEnumerable<BomAllocationResponse>> GetBomAllocationAsync(Guid inventoryId); 
    IEnumerable<BomAllocationResponse> GetBomAllocation(Guid inventoryId);

}

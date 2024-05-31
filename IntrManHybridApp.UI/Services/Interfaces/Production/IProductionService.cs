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

    Task<IEnumerable<InventoryItem>> GetRawMaterialsForProductionAsync();
    Task<Guid> CreateRawMaterialsCheckout(ProductCheckOutRequest request);

    Task<IEnumerable<InventoryItem>> GetRunningProductionItemsAsync();
    Task<Guid> CreateFinishedProductCheckin(FinishedProductInternalCheckinRequest request);

}

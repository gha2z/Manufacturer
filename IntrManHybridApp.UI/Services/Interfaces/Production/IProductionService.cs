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
}

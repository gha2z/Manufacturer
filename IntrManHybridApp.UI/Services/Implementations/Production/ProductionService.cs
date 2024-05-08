using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services
{
    public class ProductionService(HttpClient httpClient, ILogger<ProductionService> logger) : IProductionService
    {
        public async Task<Guid> CreateProductionOrderAsync(ProductionOrderRequest request)
        {
            try
            {
                logger.LogInformation($"Creating Production Order: Post {httpClient.BaseAddress}/ProductionOrders");
                var response = await httpClient.PostAsJsonAsync("ProductionOrders", request);
                return response.Content.ReadFromJsonAsync<Guid>().Result;

            } catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Production Order");
                return Guid.Empty;
            }
        }

        public Task<bool> DeleteProductionOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EndProduct>> GetEndProductsAsync()
        {
            try
            {
                logger.LogInformation($"Getting End Products: Get {httpClient.BaseAddress}/endProducts");
                IEnumerable<EndProduct> response = await httpClient.GetFromJsonAsync<IEnumerable<EndProduct>>("EndProducts") ?? [];
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting End Products");
                return [];
            }
            
        }

        public Task<ProductionOrderRequest> GetProductionOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductionOrderRequest>> GetProductionOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}

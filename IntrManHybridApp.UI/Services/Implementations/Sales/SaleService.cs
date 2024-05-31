using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services
{
    public class SaleService(HttpClient httpClient, ILogger<SaleService> logger) : ISaleService
    {
        public async Task<Guid> CreateDispatchEntry(DispatchRequest request)
        {
            try
            {
                logger.LogInformation($"Creating Dispatch Entry: Post {httpClient.BaseAddress}/dispatch");
                var response = await httpClient.PostAsJsonAsync("dispatch", request);
                return response.Content.ReadFromJsonAsync<Guid>().Result;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating dispatch entry");
                return Guid.Empty;
            }
        }

        public async Task<IEnumerable<InventoryItem>> GetDispatchableProducts()
        {
            try
            {
                logger.LogInformation($"Getting End Products: Get {httpClient.BaseAddress}/DispatchableProducts");
                IEnumerable<InventoryItem> response = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItem>>("DispatchableProducts") ?? [];
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting End Products");
                return [];
            }
        }
    }
}

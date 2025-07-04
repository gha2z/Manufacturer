﻿using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using Radzen;
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

        public async Task<IEnumerable<EndProductItemDetail>> GetDispatchableProducts()
        {
            try
            {
                logger.LogInformation($"Getting End Products: Get {httpClient.BaseAddress}/DispatchableProducts");
                IEnumerable<EndProductItemDetail> response = 
                    await httpClient.GetFromJsonAsync<IEnumerable<EndProductItemDetail>>("DispatchableProducts") ?? [];
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting End Products");
                return [];
            }
        }

        public async Task<IEnumerable<DispatchOrderDetail>> GetDispatchOrderDetailByDate(DateTime date)
        {
            try
            {
                logger.LogInformation($"Getting Dispatch Order Detail: Get {httpClient.BaseAddress}/dispatchOrderDetailByDate/{date}");
                IEnumerable<DispatchOrderDetail> response =
                    await httpClient.GetFromJsonAsync<IEnumerable<DispatchOrderDetail>>($"dispatchOrderDetailByDate/{date:yyyy-MM-dd}") ?? [];
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting Dispatch Order Detail");
                return [];
            }
        }

        public async Task<IEnumerable<DispatchOrderDetail>> GetUnDispatchedOrdersAsync()
        {
            try
            {
                logger.LogInformation($"Getting Undispatched Order: Get {httpClient.BaseAddress}/undispatchedOrders");
                IEnumerable<DispatchOrderDetail> response =
                    await httpClient.GetFromJsonAsync<IEnumerable<DispatchOrderDetail>>($"undispatchedOrders") ?? [];
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting Un dispatched Orders");
                return [];
            }
        }

        public async Task<bool> SetNextInventoryDispatchStatus(NextDispatchStatusRequest request)
        {
            bool result = false;
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(request);
                logger.LogInformation($"Setting Next Inventory Dispatch Status: Post {httpClient.BaseAddress}/{json}");
                var response = await httpClient.PostAsJsonAsync($"nextDispatchStatus", request);
                result = response.Content.ReadFromJsonAsync<bool>().Result;
            } catch (Exception ex)
            {
                logger.LogError(ex, "Error setting next inventory dispatch status");
            }
            return result;
        }
    }
}

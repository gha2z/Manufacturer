using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System.Diagnostics;
using System.Net.Http.Json;

namespace IntrManHybridApp.UI.Services;

public class SupplierService(HttpClient httpClient, ILogger<SupplierService> logger) : ISupplierService
{
    public async Task<Guid> CreateSupplierAsync(SupplierRequest request)
    {
       
        try
        {
            logger.LogInformation($"Creating Supplier: Post {httpClient.BaseAddress}/suppliers");
            var response = await httpClient.PostAsJsonAsync("suppliers", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Supplier");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteSupplierAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting Supplier: Delete {httpClient.BaseAddress}/suppliers/{id}");
            var result = await httpClient.DeleteAsync($"suppliers/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting Supplier");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<SupplierResponse> GetSupplierAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting Supplier: Get {httpClient.BaseAddress}/suppliers/{id}");
            SupplierResponse response = await httpClient.GetFromJsonAsync<SupplierResponse>($"suppliers/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Supplier");
            return new();
        }
    }

    public async Task<IEnumerable<SupplierResponse>> GetSuppliersAsync()
    {
        try
        { 
            logger.LogInformation($"Getting Suppliers: Get {httpClient.BaseAddress}/suppliers");
            IEnumerable<SupplierResponse> items = await httpClient.GetFromJsonAsync<IEnumerable<SupplierResponse>>("suppliers") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Suppliers");
            return [];
        }
    }

    public async Task<Guid> UpdateSupplierAsync(SupplierRequest request)
    {
        try
        {
            logger.LogInformation($"Updating Supplier: Put {httpClient.BaseAddress}/suppliers");
            var response = await httpClient.PutAsJsonAsync("suppliers", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        } 
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating Supplier");
            return Guid.Empty;
        }
    }

}

using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Json;

namespace IntrManHybridApp.UI.Services;

public class CustomerService(HttpClient httpClient, ILogger<CustomerService> logger) : ICustomerService
{
    public async Task<Guid> CreateCustomerAsync(CustomerRequest request)
    {

        try
        {
            logger.LogInformation($"Creating Customer: Post {httpClient.BaseAddress}/Customers");
            var response = await httpClient.PostAsJsonAsync("Customers", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Customer");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting Customer: Delete {httpClient.BaseAddress}/Customers/{id}");
            var result = await httpClient.DeleteAsync($"Customers/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting Customer");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<CustomerResponse> GetCustomerAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting Customer: Get {httpClient.BaseAddress}/Customers/{id}");
            CustomerResponse response = await httpClient.GetFromJsonAsync<CustomerResponse>($"Customers/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Customer");
            return new();
        }
    }

    public async Task<IEnumerable<CustomerResponse>> GetCustomersAsync()
    {
        try
        {
            logger.LogInformation($"Getting Customers: Get {httpClient.BaseAddress}/Customers");
            IEnumerable<CustomerResponse> items = await httpClient.GetFromJsonAsync<IEnumerable<CustomerResponse>>("Customers") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Customers");
            return [];
        }
    }

    public async Task<Guid> UpdateCustomerAsync(CustomerRequest request)
    {
        try
        {
            logger.LogInformation($"Updating Customer: Put {httpClient.BaseAddress}/Customers");
            var response = await httpClient.PutAsJsonAsync("Customers", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating Customer");
            return Guid.Empty;
        }
    }

}

using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Json;

namespace IntrManHybridApp.UI.Services;

public class LocationService(HttpClient httpClient, ILogger<LocationService> logger) : ILocationService
{
    #region Location
    public async Task<Guid> CreateLocationAsync(LocationRequest request)
    {
        try
        {
            logger.LogInformation($"Creating Location: Post {httpClient.BaseAddress}/Locations");
            var response = await httpClient.PostAsJsonAsync("locations", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Location");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteLocationAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting Location: Delete {httpClient.BaseAddress}/Locations/{id}");
            var result = await httpClient.DeleteAsync($"Locations/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting Location");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<LocationResponse> GetLocationAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting Location: Get {httpClient.BaseAddress}/Locations/{id}");
            LocationResponse response = await httpClient.GetFromJsonAsync<LocationResponse>($"Locations/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Location");
            return new();
        }
    }

    public async Task<IEnumerable<LocationResponse>> GetLocationsAsync()
    {
        try
        { 
            logger.LogInformation($"Getting Locations: Get {httpClient.BaseAddress}/Locations");
            IEnumerable<LocationResponse> items = await httpClient.GetFromJsonAsync<IEnumerable<LocationResponse>>("locations") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Locations");
            return [];
        }
    }

    public async Task<Guid> UpdateLocationAsync(LocationRequest request)
    {
        try
        {
            logger.LogInformation($"Updating Location: Put {httpClient.BaseAddress}/Locations");
            var response = await httpClient.PutAsJsonAsync("locations", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        } 
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating Location");
            return Guid.Empty;
        }
    }
    #endregion

    #region RackingPallet
    public async Task<Guid> CreateRackingPalletAsync(RackingPalletRequest request)
    {
        try
        {
            logger.LogInformation($"Creating RackingPallet({request.Col},{request.Row},{request.Description}): Post {httpClient.BaseAddress}/rackingPallets");
            var response = await httpClient.PostAsJsonAsync("rackingPallets", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating RackingPallet");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteRackingPalletAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting RackingPallet: Delete {httpClient.BaseAddress}/RackingPallets/{id}");
            var result = await httpClient.DeleteAsync($"RackingPallets/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting RackingPallet");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<RackingPalletResponse> GetRackingPalletAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting RackingPallet: Get {httpClient.BaseAddress}/RackingPallets/{id}");
            RackingPalletResponse response = await httpClient.GetFromJsonAsync<RackingPalletResponse>($"rackingPallets/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting RackingPallet");
            return new();
        }
    }

    public async Task<IEnumerable<RackingPalletResponse>> GetRackingPalletsAsync()
    {
        try
        {
            logger.LogInformation($"Getting RackingPallets: Get {httpClient.BaseAddress}/RackingPallets");
            IEnumerable<RackingPalletResponse> items = await httpClient.GetFromJsonAsync<IEnumerable<RackingPalletResponse>>("RackingPallets") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting RackingPallets");
            return [];
        }
    }

    public async Task<Guid> UpdateRackingPalletAsync(RackingPalletRequest request)
    {
        try
        {
            logger.LogInformation($"Updating RackingPallet: Put {httpClient.BaseAddress}/RackingPallets");
            var response = await httpClient.PutAsJsonAsync("RackingPallets", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating RackingPallet");
            return Guid.Empty;
        }
    }
    #endregion
}

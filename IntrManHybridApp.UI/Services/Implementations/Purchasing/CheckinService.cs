using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services;

public class CheckinService(HttpClient httpClient, ILogger<CheckinService> logger) : ICheckinService
{
    public async Task<Guid> CreateCheckinAsync(ProductCheckinRequest request)
    {
        try
        {
            logger.LogInformation($"Creating Checkin: Post {httpClient.BaseAddress}/productCheckins");
            var response = await httpClient.PostAsJsonAsync<ProductCheckinRequest>("productCheckins", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;

        } catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Checkin");
            return Guid.Empty;
        }
    }

    public Task<bool> DeleteCheckinAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCheckinRequest> GetCheckinAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductCheckinRequest>> GetCheckinsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductCheckInLineDetailResponse>> GetCheckinRawMaterials(Guid id)
    {
        try
        {
            logger.LogInformation($"GetProductCheckInLineDetail: Post {httpClient.BaseAddress}/checkinRawMaterials/{id}");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductCheckInLineDetailResponse>>($"checkinRawMaterials/{id}") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting raw materials for checkin");
            return [];
        }
    }

    public async Task<IEnumerable<RawMaterialsForCheckin>> GetRawMaterialsForCheckinAsync()
    {
        try
        {
            logger.LogInformation($"GetRawMaterialsForCheckin: Post {httpClient.BaseAddress}/rawMaterialsForCheckin");
            return await httpClient.GetFromJsonAsync<IEnumerable<RawMaterialsForCheckin>>("rawMaterialsForCheckin") ?? [];
        } catch (Exception ex)
        {
            logger.LogError(ex, "Error getting raw materials for checkin");
            return [];
        }
    }
}


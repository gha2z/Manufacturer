using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services;

public class AuthService(HttpClient httpClient, ILogger<AuthService> logger) : IAuthService
{
    public async Task<List<FeatureAccess>> GetFeatures()
    {
        List<FeatureAccess> features = [];
        try
        {
            logger.LogInformation("Getting app features");
            features = await httpClient.GetFromJsonAsync<List<FeatureAccess>>("features") ?? [];
        } catch(Exception ex)
        {
            logger.LogInformation("Error Getting app features: {0}", ex.Message);
        }
        return features;
    }

    public async Task Login(string username, string password)
    {
       
        try
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Username = username,
                Password = Utility.Encrypt(password)
            };
            var json = JsonSerializer.Serialize(loginRequest);
            logger.LogInformation("Login request: {0}", json);
            var response = await httpClient.PostAsJsonAsync("login", loginRequest);
            var loginResponse = response.Content.ReadFromJsonAsync<LoginResponse>().Result ?? new();
            AppUser.Init(loginResponse.Token, loginResponse.UserId, loginResponse.Username, loginResponse.Role, loginResponse.FeatureAccesses);
        } catch (Exception ex)
        {
            logger.LogError(ex, "Error while logging in");
            AppUser.Reset();
        }
    }
}



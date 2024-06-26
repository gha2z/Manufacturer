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
            AppUser.Init(AppUser.Token, loginResponse.UserId, loginResponse.Username, loginResponse.Role, loginResponse.FeatureAccesses);
        } catch (Exception ex)
        {
            logger.LogError(ex, "Error while logging in");
            AppUser.Reset();
        }

    }
}

public class AppUser
{
    public static Guid Token { get; set; } = Guid.Empty;
    public static Guid UserId { get; set; } = Guid.Empty;
    public static string Username { get; set; } = string.Empty;
    public static string Role { get; set; } = string.Empty;
    public static List<FeatureAccess> FeatureAccesses { get; set; } = [];

    public static void Init(Guid token, Guid userId, string username, string role, List<FeatureAccess> featureAccesses)
    {
        Token = token;
        UserId = userId;
        Username = username;
        Role = role;
        FeatureAccesses = featureAccesses;
    }

    public static void Reset()
    {
        Token = Guid.Empty;
        UserId = Guid.Empty;
        Username = string.Empty;
        Role = string.Empty;
        FeatureAccesses = [];
    }
}

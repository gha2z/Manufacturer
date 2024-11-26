using IntrManApp.Shared.Common;
using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntrManHybridApp.UI.Services;

public class AuthService(HttpClient httpClient, ILogger<AuthService> logger) : IAuthService
{
    public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Changing password: {httpClient.BaseAddress}/ChangePassword/{json}");
            return await httpClient.PostAsJsonAsync("ChangePassword", request).Result.Content.ReadFromJsonAsync<bool>();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error while changing password => {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<ApplicationUserRoleResponse>> GetApplicationUserRolesAsync()
    {
        List<ApplicationUserRoleResponse> appRoles = [];
        try
        {
            logger.LogInformation($"Getting app user roles with features: {httpClient.BaseAddress}ApplicationUserRoles");
            appRoles = await httpClient.GetFromJsonAsync<List<ApplicationUserRoleResponse>>("ApplicationUserRoles") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogInformation("Error Getting app features: {0}", ex.Message);
        }
        return appRoles;
    }

    public async Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync()
    {
        List<UserRoleResponse> userRoles = [];
        try
        {
            logger.LogInformation("Getting user roles");
            userRoles = await httpClient.GetFromJsonAsync<List<UserRoleResponse>>("UserRoles") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogInformation("Error Getting user roles: {0}", ex.Message);
        }
        return userRoles;
    }

    public async Task<List<FeatureAccessResponse>> GetFeaturesAsync()
    {
        List<FeatureAccessResponse> features = [];
        try
        {
            logger.LogInformation("Getting app features");
            features = await httpClient.GetFromJsonAsync<List<FeatureAccessResponse>>("features") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogInformation("Error Getting app features: {0}", ex.Message);
        }
        return features;
    }

    public async Task LoginAsync(string username, string password)
    {

        try
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Username = username,
                Password = Utility.Encrypt(password)
            };
            var json = JsonSerializer.Serialize(loginRequest);
            //std::cout << "Login Request: << json;
            logger.LogInformation("Login request: {0}", json);
            var response = await httpClient.PostAsJsonAsync("login", loginRequest);
            var loginResponse = response.Content.ReadFromJsonAsync<LoginResponse>().Result ?? new();
            AppUser.Init(loginResponse.Token, loginResponse.UserId, loginResponse.Username, loginResponse.Role, loginResponse.FeatureAccesses);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while logging in");
            AppUser.Reset();
        }
    }

    public async Task<bool> SetFeatureAccessAsync(SetFeatureAccessRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Set Feature Access: {httpClient.BaseAddress}/SetFeatureAccess/{json}");
            return await httpClient.PostAsJsonAsync("SetFeatureAccess", request).Result.Content.ReadFromJsonAsync<bool>();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error while changing password => {ex.Message}");
            return false;
        }
    }

    public async Task<Guid> NewUserAsync(NewUserRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"New user: Post {httpClient.BaseAddress}/Users/{json}");
            return await httpClient.PostAsJsonAsync("Users", request).Result.Content.ReadFromJsonAsync<Guid>();
           
        }
        catch (Exception ex)
        {
            logger.LogError($"Error creating user => {ex.Message}");
            return Guid.Empty;
        }
    }

    public async Task<Guid> UpdateUserAsync(UpdateUserRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Update user: Put {httpClient.BaseAddress}/Users/{json}");
            return await httpClient.PutAsJsonAsync("Users", request).Result.Content.ReadFromJsonAsync<Guid>();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error updating user => {ex.Message}");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting User: Delete {httpClient.BaseAddress}/Users/{id}");
            var result = await httpClient.DeleteAsync($"Users/{id}");
            logger.LogInformation($"result: {result}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting User");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<Guid> NewOrUpdateRoleAsync(NewOrUpdateRoleRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            if (request.Id == Guid.Empty)
            {

                logger.LogInformation($"New user role: Post {httpClient.BaseAddress}/UserRoles/{json}");
                return await httpClient.PostAsJsonAsync("UserRoles", request).Result.Content.ReadFromJsonAsync<Guid>();
            }
            else
            {

                logger.LogInformation($"Update user role: Put {httpClient.BaseAddress}/UserRoles/{json}");
                var response = await httpClient.PutAsJsonAsync("UserRoles", request);
                return response.Content.ReadFromJsonAsync<Guid>().Result;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error on creating/updating user => {ex.Message}");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteRoleAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting User Role: Delete {httpClient.BaseAddress}/UserRoles/{id}");
            var result = await httpClient.DeleteAsync($"UserRoles/{id}");
            logger.LogInformation($"result: {result}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting User role");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}



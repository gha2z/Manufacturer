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

public class AppCustomConfig(HttpClient client, ILogger<AppCustomConfig> logger) : IAppCustomConfig
{
    public async Task<BackupRestoreDbResult> BackupDatabaseAsync()
    {
        try
        {
            logger.LogInformation($"Backup database: {client.BaseAddress}/BackupDatabase");
            var result = await client.PostAsJsonAsync("BackupDatabase", new object());
            return result.Content.ReadFromJsonAsync<BackupRestoreDbResult>().Result ?? new BackupRestoreDbResult();
        } catch (Exception ex)
        {
            logger.LogError(ex, "BackupDatabase");
            return new BackupRestoreDbResult();
        }
    }

    public async Task<bool> CreateDatabaseAsync()
    {
        try
        {
            logger.LogInformation($"Create database: {client.BaseAddress}/CreateDatabase");
            var result = await client.PostAsJsonAsync("CreateDatabase", new object());
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BackupDatabase");
            return false;
        }
    }

    public async Task<bool> ResetTransactionAsync()
    {
        try
        {
            logger.LogInformation($"Reset database: {client.BaseAddress}/ResetTransaction");
            var result = await client.PostAsJsonAsync("ResetTransaction", new object());
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BackupDatabase");
            return false;
        }
    }

    public async Task<BackupRestoreDbResult> RestoreDatabaseAsync(RestoreDatabaseRequest request)
    {
        try
        {
            logger.LogInformation($"Restore Database: {client.BaseAddress}/RestoreDatabase/{JsonSerializer.Serialize(request)}");
            var result = await client.PostAsJsonAsync("RestoreDatabase", request);
            return result.Content.ReadFromJsonAsync<BackupRestoreDbResult>().Result ?? new BackupRestoreDbResult();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BackupDatabase");
            return new BackupRestoreDbResult();
        }
    }

    public async Task<bool> SetBackupDiskPathAsync(SetBackupDiskPathRequest request)
    {
        try
        {
            logger.LogInformation($"SetBackupDiskPath: {client.BaseAddress}/Set");
            var result = await client.PostAsJsonAsync("SetBackupDiskPath", request);
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BackupDatabase");
            return false;
        }
    }

    public async Task GetDatabaseSettingsAsync()
    {
        try
        {
            logger.LogInformation($"SetBackupDiskPath: {client.BaseAddress}/GetDatabaseSetting");
            var result = await client.GetFromJsonAsync<ServerSettingResponse>("GetDatabaseSetting") ?? new();
            DatabaseSettings.DatabaseServer = result.DatabaseServer;
            DatabaseSettings.UserId = result.UserId;
            DatabaseSettings.Password = result.Password;
            DatabaseSettings.BackupPath = result.BackupPath;
            DatabaseSettings.BackupFileName = result.BackupFileName;
            DatabaseSettings.AppendDateTime = result.AppendDateTime;
            DatabaseSettings.UseIntegratedSecurity = result.UseIntegratedSecurity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "GetDatabase Settings");
        }
    }

    public async Task<bool> SetDatabaseServerAsync(SetDatabaseServerRequest request)
    {
        try
        {
            logger.LogInformation($"SetDatabaseServer: {client.BaseAddress}/SetDatabaseServer");
            var result = await client.PostAsJsonAsync("SetDatabaseServer", request);
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "SetDatabaseServer");
            return false;
        }
    }
}

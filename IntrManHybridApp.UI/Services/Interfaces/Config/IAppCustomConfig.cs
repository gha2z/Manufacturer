using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services;

public interface IAppCustomConfig
{
    Task<bool> CreateDatabaseAsync();
    Task<BackupRestoreDbResult> BackupDatabaseAsync();
    Task<BackupRestoreDbResult> RestoreDatabaseAsync(RestoreDatabaseRequest request);
    Task<bool> SetBackupDiskPathAsync(SetBackupDiskPathRequest request);
    Task<bool> ResetTransactionAsync();
    Task GetDatabaseSettingsAsync();
    Task<bool> SetDatabaseServerAsync(SetDatabaseServerRequest request);
}

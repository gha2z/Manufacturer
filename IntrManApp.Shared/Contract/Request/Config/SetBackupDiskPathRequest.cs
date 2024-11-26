namespace IntrManApp.Shared.Contract;

public class SetBackupDiskPathRequest
{
    public string Db { get; set; } = "Gha2zERPDB";
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = "Gha2zERPDBBackup";
    public bool AppendDateTime { get; set; } = true;
}

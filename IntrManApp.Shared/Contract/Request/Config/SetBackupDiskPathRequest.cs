namespace IntrManApp.Shared.Contract;

public class SetBackupDiskPathRequest
{
    public string Db { get; set; } = "IntrManDb";
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = "IntrManDbBackup";
    public bool AppendDateTime { get; set; } = true;
}

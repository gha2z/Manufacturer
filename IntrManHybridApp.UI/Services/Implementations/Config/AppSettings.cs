namespace IntrManHybridApp.UI.Services
{
    public class AppSettings
    {
        public static string ApiUrlVerb { get; set; } = "http";
        public static string ApiBaseUrl { get; set; } = string.Empty;
        public static int ApiBasePort { get; set; } = 39501;
        public static string AppDataPath { get; set; } = string.Empty;
    }

    public class DatabaseSettings
    {
        public static string DatabaseServer { get; set; } = string.Empty;
        public static string UserId { get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;
        public static string BackupPath { get; set; } = string.Empty;
        public static string BackupFileName { get; set; } = string.Empty;
        public static bool AppendDateTime { get; set; } = true;
        public static bool UseIntegratedSecurity { get; set;} = true; 
        public static int Port { get; set; } = 39501;
    }
}



namespace IntrManApp.Shared.Common;

public class ServerAppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
    public SerilogConfig Serilog { get; set; } = new SerilogConfig();
    public string AllowedHosts { get; set; } = "*";
    public Int32 Port { get; set; } = 39501;

    public class SerilogConfig
    {
        public string[] Using { get; set; } = ["Serilog.Sinks.Console", "Serilog.Sinks.File"];
        public SerilogLogLevel MinimumLevel { get; set; } = new SerilogLogLevel();
        public string[] Enrich { get; set; } = ["FromLogContext", "WithMachineName", "WithThreadId"];
        public LogWriteDefinition[] WriteTo { get; set; } = [new LogWriteDefinition()];

        public class SerilogLogLevel
        {
            public string Default { get; set; } = "Information";
            public LogOverride Override { get; set; } = new LogOverride();

            public class LogOverride
            {
                public string Microsoft { get; set; } = "Warning";
                public string System { get; set; } = "Warning";
            }
        }

        public class LogWriteDefinition
        {
            public string Name { get; set; } = "File";
            public FileArgs Args { get; set; } = new();

            public class FileArgs
            {
                public string Path { get; set; } = "Logs/log.txt";
                public string RollingInterval { get; set; } = "Day";
                public bool RollOnFileSizeLimit { get; set; } = true;
                public string Formatter { get; set; } = "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact";
            }
        }
    }
}

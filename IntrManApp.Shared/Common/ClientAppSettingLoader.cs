﻿namespace IntrManApp.Shared.Common
{
    public class ClientAppSettingLoader
    {
        public string ApiUrlVerb { get; set; } = "http";
        public string ApiBaseUrl { get; set; } = string.Empty;
        public int ApiBasePort { get; set; } = 39501;
        public string AppDataPath { get; set; } = string.Empty;
    }
}

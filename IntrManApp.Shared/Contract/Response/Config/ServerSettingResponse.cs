using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class ServerSettingResponse
{
    public  string DatabaseServer { get; set; } = string.Empty;
    public  string UserId { get; set; } = string.Empty;
    public  string Password { get; set; } = string.Empty;
    public  string BackupPath { get; set; } = string.Empty;
    public  string BackupFileName { get; set; } = string.Empty;
    public  bool AppendDateTime { get; set; } = true;
    public bool UseIntegratedSecurity { get; set; } = true;
    public int Port { get; set; } = 50001;


}

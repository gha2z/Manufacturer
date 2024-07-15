using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class BackupRestoreDbResult
{
    public string Path { get; set; } = string.Empty;
    public bool IsSucceeded { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

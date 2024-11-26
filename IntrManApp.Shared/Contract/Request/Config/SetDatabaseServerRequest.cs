using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class SetDatabaseServerRequest
{
    public string Server { get; set; } = "localhost";
    public string UserId { get; set; } = "";
    public string Password { get; set; } = "";
    public bool UseIntegratedSecurity { get; set; } = true;
    public int Port { get; set; } = 39501;
}

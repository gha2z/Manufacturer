using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class RestoreDatabaseRequest
{
    public string DB { get; set; } = "IntrManDb";
    public string Path { get; set; } = string.Empty;
}

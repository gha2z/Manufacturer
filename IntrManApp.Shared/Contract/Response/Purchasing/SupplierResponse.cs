using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class SupplierResponse
{
    public Guid BusinessEntityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

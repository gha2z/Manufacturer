using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class SetFeatureAccessRequest
{
    public Guid FeatureId { get; set; } = Guid.Empty;
    public Guid UserTypeId { get; set; } = Guid.Empty;
    public bool CanView { get; set; } = false;
}

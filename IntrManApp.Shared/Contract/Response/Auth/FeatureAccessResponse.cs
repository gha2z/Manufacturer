using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class FeatureAccessResponse
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public bool CanView { get; set; } = false;
    public string Path { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;

    public Guid RoleId { get; set; } = Guid.Empty;
    public List<FeatureAccessResponse> ChildrenFeatures { get; set; } = [];
}

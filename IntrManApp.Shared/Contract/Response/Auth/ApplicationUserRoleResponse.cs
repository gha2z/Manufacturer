using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class ApplicationUserRoleResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public List<FeatureAccessResponse> FeatureAccess { get; set; } = [];
        public List<ApplicationUserResponse> ApplicationUsers { get; set; } = [];

    }
}

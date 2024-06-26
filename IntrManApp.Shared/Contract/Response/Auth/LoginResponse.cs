using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract
{
    public class LoginResponse
    {
        public Guid Token { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<FeatureAccess> FeatureAccesses { get; set; } = [];
    }
}

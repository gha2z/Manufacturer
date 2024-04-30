using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract.Request.Tools
{
    public class BTPrintResponse
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? StatusUrl {get; set;}
        public IEnumerable<string>? Messages { get; set; }
    }
}

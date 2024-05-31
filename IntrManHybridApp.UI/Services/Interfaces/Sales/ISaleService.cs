using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services
{
    public interface ISaleService
    {
        Task<Guid> CreateDispatchEntry(DispatchRequest request);
        Task<IEnumerable<InventoryItem>> GetDispatchableProducts();
    }
}

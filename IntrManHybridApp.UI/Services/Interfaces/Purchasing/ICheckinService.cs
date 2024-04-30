using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services;

public interface ICheckinService
{
    Task<Guid> CreateCheckinAsync(ProductCheckinRequest request);
    Task<bool> DeleteCheckinAsync(Guid id);
    Task<ProductCheckinRequest> GetCheckinAsync(Guid id);
    Task<IEnumerable<ProductCheckinRequest>> GetCheckinsAsync();
    Task<IEnumerable<RawMaterialsForCheckin>> GetRawMaterialsForCheckinAsync();
    Task<IEnumerable<ProductCheckInLineDetailResponse>> GetCheckinRawMaterials(Guid id);
}

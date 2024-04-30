using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services;

    public interface ILocationService
    {
        // Location
        Task<Guid> CreateLocationAsync(LocationRequest request);
        Task<Guid> UpdateLocationAsync(LocationRequest request);
        Task<bool> DeleteLocationAsync(Guid id);
        Task<IEnumerable<LocationResponse>> GetLocationsAsync();
        Task<LocationResponse> GetLocationAsync(Guid id);
        
        // RackingPallet
        Task<Guid> CreateRackingPalletAsync(RackingPalletRequest request);
        Task<Guid> UpdateRackingPalletAsync(RackingPalletRequest request);
        Task<bool> DeleteRackingPalletAsync(Guid id);
        Task<IEnumerable<RackingPalletResponse>> GetRackingPalletsAsync();
        Task<RackingPalletResponse> GetRackingPalletAsync(Guid id);

    }


using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services;

    public interface ISupplierService
    {
        Task<Guid> CreateSupplierAsync(SupplierRequest request);
        Task<Guid> UpdateSupplierAsync(SupplierRequest request);
        Task<bool> DeleteSupplierAsync(Guid id);
        Task<IEnumerable<SupplierResponse>> GetSuppliersAsync();
        Task<SupplierResponse> GetSupplierAsync(Guid id);
    }


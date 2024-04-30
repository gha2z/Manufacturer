using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(CustomerRequest request);
    Task<Guid> UpdateCustomerAsync(CustomerRequest request);
    Task<bool> DeleteCustomerAsync(Guid id);
    Task<IEnumerable<CustomerResponse>> GetCustomersAsync();
    Task<CustomerResponse> GetCustomerAsync(Guid id);
}


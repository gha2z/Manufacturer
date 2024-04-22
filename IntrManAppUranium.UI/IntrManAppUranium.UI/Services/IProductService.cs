using IntrManHyridApp.Shared.Contract;

namespace IntrManAppUranium.UI.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetProductsAsync();
    Task<ProductResponse> CreateProductAsync(CreateProductRequest product);
    Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product);
    Task<bool> DeleteProductAsync(Guid id);
}

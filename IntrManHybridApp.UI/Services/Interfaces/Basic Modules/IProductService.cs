using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services;

public interface IProductService
{
    Task<Guid> CreateProductAsync(ProductRequest request);
    Task<Guid> UpdateProductAsync(ProductRequest request);
    Task<bool> DeleteProductAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<IEnumerable<RawMaterialBasicInfo>> GetRawMaterialsBasicInfoAsync();
    Task<ProductRequest> GetProductAsync(Guid id);
    Task<ProductCategoryResponse> GetProductCategoryAsync(Guid id);
    Task<IEnumerable<ProductCategoryResponse>> GetProductCategoriesAsync();
    Task<Guid> CreateProductCategoryAsync(ProductCategoryRequest request);
    Task<Guid> UpdateProductCategoryAsync(ProductCategoryRequest request);
    Task<bool> DeleteProductCategoryAsync(Guid id);
    Task<List<BomSpecificationResponse>> GetBomSpecificationAsync(Guid ProductId);
    List<BomSpecificationResponse> GetBomSpecification(Guid id);
    Task<bool> CreateBillOfMaterialAsync(BomRequest request);
    Task<List<MeasurementUnitRequest>> GetMeasurementUnitAsync();


}

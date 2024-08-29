using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntrManHybridApp.UI.Services;



public class ProductService(HttpClient httpClient, ILogger<ProductService> logger) : IProductService
{
    #region Product
    public async Task<Guid> CreateProductAsync(ProductRequest request)
    {

        try
        {
            string message = $"Creating Product: Post {httpClient.BaseAddress}/products/{JsonSerializer.Serialize(request)}";
            logger.LogInformation(message: message);

            var response = await httpClient.PostAsJsonAsync("products", request);
            
            logger.LogInformation($"response: {response}");
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Product");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting Product: Delete {httpClient.BaseAddress}/products/{id}");
            var result = await httpClient.DeleteAsync($"Products/{id}");
            logger.LogInformation($"result: {result}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting Product");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<ProductRequest> GetProductAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting Product: Get {httpClient.BaseAddress}/products/{id}");
            ProductRequest response = await httpClient.GetFromJsonAsync<ProductRequest>($"products/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Product");
            return new();
        }
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        try
        {
            string message = $"Getting Products: Get {httpClient.BaseAddress}/products";
            logger.LogInformation(message: message);
            IEnumerable<ProductResponse> items = await httpClient.GetFromJsonAsync<IEnumerable<ProductResponse>>("products") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Products");
            return [];
        }
    }

    public async Task<Guid> UpdateProductAsync(ProductRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Updating Product: Put {httpClient.BaseAddress}/products/{JsonSerializer.Serialize(request)}" +
                $"{Environment.NewLine}{json}");
            var response = await httpClient.PutAsJsonAsync("products", request);
            logger.LogInformation($"response: {response}");
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating Product");
            return Guid.Empty;
        }
    }
    #endregion

    #region ProductCategory
    public async Task<Guid> CreateProductCategoryAsync(ProductCategoryRequest request)
    {

        try
        {
            logger.LogInformation($"Creating Product Category: Post {httpClient.BaseAddress}/productCategories");
            var response = await httpClient.PostAsJsonAsync("productCategories", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Product Category");
            return Guid.Empty;
        }
    }

    public async Task<bool> DeleteProductCategoryAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting Product Category: Delete {httpClient.BaseAddress}/productCategories/{id}");
            var result = await httpClient.DeleteAsync($"productCategories/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting Product Category");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<ProductCategoryResponse> GetProductCategoryAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting Product Category: Get {httpClient.BaseAddress}/productCategories/{id}");
            ProductCategoryResponse response = await httpClient.GetFromJsonAsync<ProductCategoryResponse>($"productCategories/{id}") ?? new();
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Product Category");
            return new();
        }
    }

    public async Task<IEnumerable<ProductCategoryResponse>> GetProductCategoriesAsync()
    {
        try
        {
            logger.LogInformation($"Getting ProductCategorys: Get {httpClient.BaseAddress}/productCategories");
            IEnumerable<ProductCategoryResponse> items = 
                await httpClient.GetFromJsonAsync<IEnumerable<ProductCategoryResponse>>("productCategories") ?? [];
            return items;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Product Categories");
            return [];
        }
    }

    public async Task<Guid> UpdateProductCategoryAsync(ProductCategoryRequest request)
    {
        try
        {
            logger.LogInformation($"Updating ProductCategory: Put {httpClient.BaseAddress}/productCategories");
            var response = await httpClient.PutAsJsonAsync("productCategories", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating Product Category");
            return Guid.Empty;
        }
    }
    #endregion

    #region BillOfMaterial

    public async Task<bool> CreateBillOfMaterialAsync(BomRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Creating BillOfMaterial for {request.ProductId}: Post {httpClient.BaseAddress}/boms. Request:{json}");
           
            var response = await httpClient.PostAsJsonAsync("boms", request);
            return response.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating BillOfMaterial");
            return false;
        }
    }

    

    public async Task<bool> DeleteBillOfMaterialAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Deleting BillOfMaterial: Delete {httpClient.BaseAddress}/boms/{id}");
            var result = await httpClient.DeleteAsync($"boms/{id}");
            return result.Content.ReadFromJsonAsync<bool>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting BillOfMaterial");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<BomSpecificationResponse>> GetBomSpecificationAsync(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting BillOfMaterial: Get {httpClient.BaseAddress}/boms/{id}");
            List<BomSpecificationResponse> response = await httpClient.GetFromJsonAsync<List<BomSpecificationResponse>>($"boms/{id}") ?? [];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting BillOfMaterial");
            return [];
        }
    }

    public List<BomSpecificationResponse> GetBomSpecification(Guid id)
    {
        try
        {
            logger.LogInformation($"Getting BillOfMaterial: Get {httpClient.BaseAddress}/boms/{id}");
            List<BomSpecificationResponse> response = httpClient.GetFromJsonAsync<List<BomSpecificationResponse>>($"boms/{id}").Result ?? [];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting BillOfMaterial");
            return [];
        }
    }

    public async Task<IEnumerable<RawMaterialBasicInfo>> GetRawMaterialsBasicInfoAsync()
    {
        try
        {
            logger.LogInformation($"Getting List of Id and Names of Raw Materials: Get {httpClient.BaseAddress}/rawMaterialsBasicInfo");
            IEnumerable<RawMaterialBasicInfo> response = await httpClient.GetFromJsonAsync<IEnumerable<RawMaterialBasicInfo>>($"rawMaterialsBasicInfo") ?? [];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting List of Id and Names of Raw Materials");
            return [];
        }
    }

    public async Task<List<MeasurementUnitRequest>> GetMeasurementUnitAsync()
    {
        try
        {
            logger.LogInformation("Getting Measurement Units");
            return await httpClient.GetFromJsonAsync<List<MeasurementUnitRequest>>("measurementUnits") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Measurement Units");
            return [];
        }
    }
    #endregion
}

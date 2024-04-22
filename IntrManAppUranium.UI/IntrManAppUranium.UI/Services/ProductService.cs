using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;


public class ProductService : IProductService
{
    IHttpClientFactory clientFactory;
    string _baseUrl;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        clientFactory = httpClientFactory;
        _baseUrl = "https://localhost:7274/api"; // "http://localhost:5096"; // 
        
    }

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        List<ProductResponse>? products=new();
        //using HttpClient client = clientFactory.CreateClient();
        try
        {
            //var response = await client.GetAsync($"{_baseUrl}/products");

            //if (response.IsSuccessStatusCode)
            //{
            //var content = await response.Content.ReadAsStringAsync();
            //products = await response.Content.ReadFromJsonAsync(ProductContext.Default.ListProductResponse);
            //JsonSerializer.Deserialize<List<ProductResponse>>(content);
            //}
            products.Add(new ProductResponse { Id = Guid.NewGuid(), ProductNumber = "001", Names = "Product 1", CategoryName = "Cat 1" });
            products.Add(new ProductResponse { Id = Guid.NewGuid(), ProductNumber = "002", Names = "Product 2", CategoryName = "Cat 1" });
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        if(products==null) products=[];
        Debug.WriteLine(products.Count);
      
        return products;
    }

    public async Task<ProductResponse> CreateProductAsync(CreateProductRequest productRequest)
    {
        using HttpClient client = clientFactory.CreateClient();
        var content = new StringContent(JsonSerializer.Serialize(productRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{_baseUrl}/products", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductResponse>(responseContent);

        return product;
    }

    public async Task<ProductResponse> UpdateProductAsync(UpdateProductRequest productRequest)
    {
        using HttpClient client = clientFactory.CreateClient();
        var content = new StringContent(JsonSerializer.Serialize(productRequest), Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{_baseUrl}/products/", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductResponse>(responseContent);

        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        using HttpClient client = clientFactory.CreateClient();
        var response = await client.DeleteAsync($"{_baseUrl}/products/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}

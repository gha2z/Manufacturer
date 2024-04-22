using IntrManHyridApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHyridApp.UI.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProductsAsync();
        Task<ProductResponse> CreateProductAsync(CreateProductRequest product);
        Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}

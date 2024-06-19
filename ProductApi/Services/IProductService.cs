using ProductApi.DTOs;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);
    }  
}

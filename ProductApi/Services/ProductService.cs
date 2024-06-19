using ProductApi.DTOs;
using ProductApi.Models;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return null;

            // Mapping and getting discount from external service
            // Assuming GetDiscountAsync() is implemented
            var discount = await GetDiscountAsync(productId);

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                StatusName = product.Status == 1 ? "Active" : "Inactive",
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = discount,
                FinalPrice = product.Price * (100 - discount) / 100
            };
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Status = productDto.StatusName == "Active" ? 1 : 0,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Price = productDto.Price
            };

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Status = productDto.StatusName == "Active" ? 1 : 0,
                Stock = productDto.Stock,
                Description = productDto.Description,
                Price = productDto.Price
            };

            await _productRepository.UpdateAsync(product);
        }

        private Task<int> GetDiscountAsync(int productId)
        {
            // Simulate external service call
            return Task.FromResult(10); // Dummy value
        }
    }

}

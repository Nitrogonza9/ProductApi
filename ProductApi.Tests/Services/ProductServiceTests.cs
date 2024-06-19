using Xunit;
using Moq;
using ProductApi.Repositories;
using ProductApi.Services;
using ProductApi.Models;
using ProductApi.Data;
using System.Threading.Tasks;
using ProductApi.DTOs;

namespace ProductApi.Tests.Services
{
    // Services/ProductServiceTests.cs


    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProductDto()
        {
            // Arrange
            var productId = 1;
            var product = new Product
            {
                ProductId = productId,
                Name = "Test Product",
                Status = 1,
                Stock = 100,
                Description = "Test Description",
                Price = 100.0M
            };

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public async Task AddProductAsync_CallsRepository()
        {
            // Arrange
            var productDto = new ProductDto
            {
                ProductId = 1,
                Name = "New Product",
                StatusName = "Active",
                Stock = 50,
                Description = "New Description",
                Price = 200.0M
            };

            // Act
            await _productService.AddProductAsync(productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }

}

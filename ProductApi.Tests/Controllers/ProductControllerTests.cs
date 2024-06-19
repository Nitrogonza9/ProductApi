using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Tests.Controllers
{
    // Controllers/ProductControllerTests.cs
    using Xunit;
    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using ProductApi.Controllers;
    using ProductApi.Services;
    using ProductApi.DTOs;
    using System.Threading.Tasks;

    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto
            {
                ProductId = productId,
                Name = "Test Product",
                StatusName = "Active",
                Stock = 100,
                Description = "Test Description",
                Price = 100.0M,
                Discount = 10,
                FinalPrice = 90.0M
            };

            _productServiceMock.Setup(service => service.GetProductByIdAsync(productId)).ReturnsAsync(productDto);

            // Act
            var result = await _productController.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(productId, returnValue.ProductId);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
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
            var result = await _productController.Post(productDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_productController.GetById), createdAtActionResult.ActionName);
            Assert.Equal(productDto.ProductId, ((ProductDto)createdAtActionResult.Value).ProductId);
        }
    }

}

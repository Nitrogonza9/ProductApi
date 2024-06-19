using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Tests.Repositories
{
    // Repositories/ProductRepositoryTests.cs
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using ProductApi.Repositories;
    using ProductApi.Models;
    using System.Threading.Tasks;
    using ProductApi.Data;

    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public ProductRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddAsync_AddsProductToDatabase()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new ProductRepository(context);
            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Status = 1,
                Stock = 100,
                Description = "Test Description",
                Price = 100.0M
            };

            // Act
            await repository.AddAsync(product);
            var addedProduct = await context.Products.FindAsync(product.ProductId);

            // Assert
            Assert.NotNull(addedProduct);
            Assert.Equal("Test Product", addedProduct.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsProduct()
        {
            // Arrange
            using var context = new ApplicationDbContext(_dbContextOptions);
            var repository = new ProductRepository(context);
            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                Status = 1,
                Stock = 100,
                Description = "Test Description",
                Price = 100.0M
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(product.ProductId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.ProductId, result.ProductId);
        }
    }

}

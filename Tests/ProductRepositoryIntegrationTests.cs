using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Test;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("Database Collection")]
    public class ProductRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _productRepository = new ProductRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        private async Task<Product> CreateSampleProductAsync(string name = "Sample Product", int price = 100)
        {
            var category = new Category { Name = "General" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = name,
                CategoryId = category.CategoryId,
                Price = price,
                Description = "Test description",
                ImageUrl = "http://example.com/image.png"
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
        [Fact]
        public async Task AddNewProduct_ShouldAddProductSuccessfully()
        {
            var category = new Category { Name = "NewCategory" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "New Product",
                CategoryId = category.CategoryId,
                Price = 150,
                Description = "A new product",
                ImageUrl = "http://example.com/new.png"
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var savedProduct = await _dbContext.Products.FindAsync(product.ProductId);
            Assert.NotNull(savedProduct);
            Assert.Equal("New Product", savedProduct.Name);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnAllProducts_WhenNoFilterApplied()
        {
            // יצירת כמה מוצרים לדוגמה
            var product1 = await CreateSampleProductAsync("Prod1", 50);
            var product2 = await CreateSampleProductAsync("Prod2", 100);
            var product3 = await CreateSampleProductAsync("Prod3", 150);

            var result = await _productRepository.GetProducts(null, null, null, null, null, false);

            Assert.NotNull(result);
            Assert.True(result.Count >= 3); // לפחות שלושה מוצרים קיימים
            Assert.Contains(result, p => p.ProductId == product1.ProductId);
            Assert.Contains(result, p => p.ProductId == product2.ProductId);
            Assert.Contains(result, p => p.ProductId == product3.ProductId);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnProducts_WhenExist()
        {
            var product = await CreateSampleProductAsync();
            var result = await _productRepository.GetProducts(null, null, null, null, null, false);

            Assert.NotNull(result);
            Assert.Contains(result, p => p.ProductId == product.ProductId);
        }

        //[Fact]
        //public async Task GetProducts_ShouldFilterByCategory()
        //{
        //    var category1 = new Category { Name = "Cat1" };
        //    var category2 = new Category { Name = "Cat2" };
        //    await _dbContext.Categories.AddRangeAsync(category1, category2);
        //    await _dbContext.SaveChangesAsync();

        //    var product1 = new Product { Name = "P1", CategoryId = category1.CategoryId, Price = 50 };
        //    var product2 = new Product { Name = "P2", CategoryId = category2.CategoryId, Price = 100 };
        //    await _dbContext.Products.AddRangeAsync(product1, product2);
        //    await _dbContext.SaveChangesAsync();

        //    var result = await _productRepository.GetProducts(new int[] { category1.CategoryId }, null, null, null, null, false);

        //    Assert.Single(result);
        //    Assert.Equal(product1.ProductId, result[0].ProductId);
        //}

        //[Fact]
        //public async Task GetProducts_ShouldFilterByPriceRange()
        //{
        //    var product1 = await CreateSampleProductAsync("Cheap", 50);
        //    var product2 = await CreateSampleProductAsync("Expensive", 200);

        //    var result = await _productRepository.GetProducts(null, 100, 300, null, null, false);

        //    Assert.Single(result);
        //    Assert.Equal(product2.ProductId, result[0].ProductId);
        //}

        //[Fact]
        //public async Task GetProducts_ShouldOrderDescending_WhenDescIsTrue()
        //{
        //    var product1 = await CreateSampleProductAsync("A", 50);
        //    var product2 = await CreateSampleProductAsync("B", 100);

        //    var result = await _productRepository.GetProducts(null, null, null, null, null, true);

        //    Assert.Equal(product2.ProductId, result.First().ProductId);
        //}

        //[Fact]
        //public async Task GetProducts_ShouldSupportPagination()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        await CreateSampleProductAsync($"Product{i}", 50 + i * 10);
        //    }

        //    var result = await _productRepository.GetProducts(null, null, null, 2, 2, false); // page 2, limit 2

        //    Assert.Equal(2, result.Count);
        //}
    }
}

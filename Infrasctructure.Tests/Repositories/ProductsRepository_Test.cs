using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class ProductsRepository_Test
{
    private readonly ILogger _logger;
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly ProductEntity demoProduct = new()
    {
        Id = "A100",
        ProductName = "Produktnamn",
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistProduct_Return_True()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);
        await productsRepository.CreateAsync(demoProduct);

        //Act
        bool result = await productsRepository.ExistsAsync(x => x.Id == demoProduct.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateProductEntityInDb_Return_ProductEntityWithIdOne()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);

        //Act
        ProductEntity result = await productsRepository.CreateAsync(demoProduct);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("A100", result.Id);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllProducts_Return_IEnumerableOfProductEntities()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);

        //Act
        IEnumerable<ProductEntity> result = await productsRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadProductsById_Return_OneProductEntity()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);
        await productsRepository.CreateAsync(demoProduct);

        //Act
        ProductEntity result = await productsRepository.ReadOneAsync(x => x.Id == demoProduct.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoProduct.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingProduct_Return_UpdatedEntity()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);
        ProductEntity productEntity = await productsRepository.CreateAsync(demoProduct);

        //Act
        productEntity.ProductName = "A150";
        ProductEntity result = await productsRepository.UpdateAsync(x => x.Id == productEntity.Id, productEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.Id, result.Id);
        Assert.Equal("A150", result.ProductName);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneProductById_Return_True()
    {
        //Arrange
        ProductsRepository productsRepository = new(_context, _logger);
        await productsRepository.CreateAsync(demoProduct);

        //Act
        bool result = await productsRepository.DeleteAsync(x => x.Id == demoProduct.Id);

        //Assert
        Assert.True(result);
    }
}

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class ProductDetailsRepository_Test
{
    private readonly ILogger _logger;
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly ProductDetailEntity demoProductDetails = new()
    {
        ProductId = "A100",
        BrandId = 1,
        UnitPrice = 100,
        Color = "Blue",
        Size = "Small"
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistProductDetails_Return_True()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        await productDetailsRepository.CreateAsync(demoProductDetails);

        //Act
        bool result = await productDetailsRepository.ExistsAsync(x => x.ProductId == demoProductDetails.ProductId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateProductDetailEntityInDb_Return_ProductDetailEntityWithIdOne()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);

        //Act
        ProductDetailEntity result = await productDetailsRepository.CreateAsync(demoProductDetails);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("A100", result.ProductId);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllProductDetails_Return_IEnumerableOfProductDetailEntities()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);

        //Act
        IEnumerable<ProductDetailEntity> result = await productDetailsRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductDetailEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadProductDetailById_Return_OneProductDetailEntity()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        await productDetailsRepository.CreateAsync(demoProductDetails);

        //Act
        ProductDetailEntity result = await productDetailsRepository.ReadOneAsync(x => x.ProductId == demoProductDetails.ProductId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoProductDetails.ProductId, result.ProductId);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingProductDetail_Return_UpdatedEntity()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductDetailEntity productDetailEntity = await productDetailsRepository.CreateAsync(demoProductDetails);

        //Act
        productDetailEntity.Color = "Yellow";
        ProductDetailEntity result = await productDetailsRepository.UpdateAsync(x => x.ProductId == productDetailEntity.ProductId, productDetailEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(productDetailEntity.ProductId, result.ProductId);
        Assert.Equal("Yellow", result.Color);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneProductDetailById_Return_True()
    {
        //Arrange
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        await productDetailsRepository.CreateAsync(demoProductDetails);

        //Act
        bool result = await productDetailsRepository.DeleteAsync(x => x.ProductId == demoProductDetails.ProductId);

        //Assert
        Assert.True(result);
    }
}

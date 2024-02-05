using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class BrandsRepository_Test
{
    private readonly ILogger _logger;
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly BrandEntity demoBrand = new()
    {
        Id = 1,
        BrandName = "Tillverkare"
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistBrand_Return_True()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        await brandsRepository.CreateAsync(demoBrand);

        //Act
        bool result = await brandsRepository.ExistsAsync(x => x.BrandName == demoBrand.BrandName);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateCBrandEntityInDb_Return_BrandEntityWithIdOne()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);

        //Act
        BrandEntity result = await brandsRepository.CreateAsync(demoBrand);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllBrands_Return_IEnumerableOfBrandEntities()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);

        //Act
        IEnumerable<BrandEntity> result = await brandsRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<BrandEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadBrandById_Return_OneBrandEntity()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        await brandsRepository.CreateAsync(demoBrand);

        //Act
        BrandEntity result = await brandsRepository.ReadOneAsync(x => x.Id == demoBrand.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoBrand.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingBrand_Return_UpdatedEntity()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        BrandEntity brandEntity = await brandsRepository.CreateAsync(demoBrand);

        //Act
        brandEntity.BrandName = "Tillverkare 2";
        BrandEntity result = await brandsRepository.UpdateAsync(x => x.Id == brandEntity.Id, brandEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(brandEntity.Id, result.Id);
        Assert.Equal("Tillverkare 2", result.BrandName);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneBrandById_Return_True()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        await brandsRepository.CreateAsync(demoBrand);

        //Act
        bool result = await brandsRepository.DeleteAsync(x => x.Id == demoBrand.Id);

        //Assert
        Assert.True(result);
    }


}

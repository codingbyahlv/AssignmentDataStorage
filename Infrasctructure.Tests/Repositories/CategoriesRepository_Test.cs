using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Infrasctructure.Tests.Repositories;

public class CategoriesRepository_Test
{
    private readonly ILogger _logger;
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);
    private readonly CategoryEntity demoCategory = new()
    {
        Id = 1,
        ParentCategoryId = 1,
        CategoryName = "Kategorinamn"
    };


    [Fact]
    public async Task ExistsAsync_Should_CheckExistCategory_Return_True()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);
        await categoriesRepository.CreateAsync(demoCategory);

        //Act
        bool result = await categoriesRepository.ExistsAsync(x => x.CategoryName == demoCategory.CategoryName);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateCategoryEntityInDb_Return_CategoryEntityWithIdOne()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);

        //Act
        CategoryEntity result = await categoriesRepository.CreateAsync(demoCategory);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ReadAllAsync_Should_ReadAllCategories_Return_IEnumerableOfCategoryEntities()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);

        //Act
        IEnumerable<CategoryEntity> result = await categoriesRepository.ReadAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryEntity>>(result);
    }

    [Fact]
    public async Task ReadOneAsync_Should_ReadCategoryById_Return_OneCategoryEntity()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);
        await categoriesRepository.CreateAsync(demoCategory);

        //Act
        CategoryEntity result = await categoriesRepository.ReadOneAsync(x => x.Id == demoCategory.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoCategory.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateExistingCategory_Return_UpdatedEntity()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);
        CategoryEntity categoryEntity = await categoriesRepository.CreateAsync(demoCategory);

        //Act
        categoryEntity.ParentCategoryId = 2;
        CategoryEntity result = await categoriesRepository.UpdateAsync(x => x.Id == categoryEntity.Id, categoryEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Id, result.Id);
        Assert.Equal(2, result.ParentCategoryId);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveOneCategoryById_Return_True()
    {
        //Arrange
        CategoriesRepository categoriesRepository = new(_context, _logger);
        await categoriesRepository.CreateAsync(demoCategory);

        //Act
        bool result = await categoriesRepository.DeleteAsync(x => x.Id == demoCategory.Id);

        //Assert
        Assert.True(result);
    }
}

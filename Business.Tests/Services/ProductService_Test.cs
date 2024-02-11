using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Business.Tests.Services;

public class ProductService_Test
{
    private readonly ILogger _logger;
    private readonly ProductCatalogContext _context = new(new DbContextOptionsBuilder<ProductCatalogContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    private readonly ProductRegistrationDto demoRegDto = new() 
    {
        ProductId = "A100",
        ProductName = "Produktnamn",
        UnitPrice = 100,
        Color = "Blue",
        Size = "small",
        Brand = "Tillverkare",
        Category = "Kategori 1",
        ParentCategoryId = 1,
    };

    private readonly ProductDto demoDto = new()
    {
        ProductId = "A100",
        ProductName = "Produktnamn2",
        UnitPrice = 100,
        Color = "Blue",
        Size = "small",
        BrandId = 1,
        BrandName = "Tillverkare2",
        CategoryId = 2,
        ParentCategoryId = 1,
        CategoryName = "Kategorinamn",
    };
    private readonly ProductDetailEntity demoDetailEntity= new()
    {
        ProductId = "A100",
        BrandId = 1,
        UnitPrice = 100,
        Color = "Blue",
        Size = "small",
    };

[Fact]
    public async Task CreateProductAsync_Should_CreateProductEntityInDb_Return_True()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        CategoriesRepository categoriesRepository = new(_context, _logger);
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductsRepository productsRepository = new(_context, _logger);
        ProductService productService = new(brandsRepository, categoriesRepository, productDetailsRepository, productsRepository, _logger);
        
        //Act
        bool result = await productService.CreateProductAsync(demoRegDto);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ReadAllProductsAsync_Should_ReadAllProducts_Return_IEnumerableOfProductDtos()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        CategoriesRepository categoriesRepository = new(_context, _logger);
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductsRepository productsRepository = new(_context, _logger);
        ProductService productService = new(brandsRepository, categoriesRepository, productDetailsRepository, productsRepository, _logger);

        //Act
        IEnumerable<ProductDto> result = await productService.ReadAllProductsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
    }

    [Fact]
    public async Task ReadOneProductAsync_Should_ReadOneProduct_Return_ProductDto()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        CategoriesRepository categoriesRepository = new(_context, _logger);
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductsRepository productsRepository = new(_context, _logger);
        ProductService productService = new(brandsRepository, categoriesRepository, productDetailsRepository, productsRepository, _logger);
        await productService.CreateProductAsync(demoRegDto);

        //Act
        ProductDto result = await productService.ReadOneProductAsync("A100");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoRegDto.ProductId, result.ProductId);
    }

    [Fact]
    public async Task UpdateOrderAsync_Should_UpdateOrder_Return_UpdatedOrderDto()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        CategoriesRepository categoriesRepository = new(_context, _logger);
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductsRepository productsRepository = new(_context, _logger);
        ProductService productService = new(brandsRepository, categoriesRepository, productDetailsRepository, productsRepository, _logger);
        await productService.CreateProductAsync(demoRegDto);
        
        //Act
        ProductDto result = await productService.UpdateProductAsync(demoDto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(demoDto.ProductName, result.ProductName);
    }

    // BUG!!!!!!!!!!!
    // delete test does not work due to unsolved bug in ProductService, check ProductService for more information
    [Fact]
    public async Task DeleteProductAsync_Should_DeleteProductFromDb_Return_True()
    {
        //Arrange
        BrandsRepository brandsRepository = new(_context, _logger);
        CategoriesRepository categoriesRepository = new(_context, _logger);
        ProductDetailsRepository productDetailsRepository = new(_context, _logger);
        ProductsRepository productsRepository = new(_context, _logger);
        ProductService productService = new(brandsRepository, categoriesRepository, productDetailsRepository, productsRepository, _logger);
        await productService.CreateProductAsync(demoRegDto);

        //Act
        bool result = await productService.DeleteProductAsync(demoDto);

        //Assert
        Assert.True(result);
    }
}

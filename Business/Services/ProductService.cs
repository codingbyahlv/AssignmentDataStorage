using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Shared.Interfaces;

namespace Business.Services;

public class ProductService(BrandsRepository brandsRepository, CategoriesRepository categoriesRepository, ProductDetailsRepository productDetailsRepository, ProductsRepository productsRepository, ILogger logger) : IProductService
{
    private readonly BrandsRepository _brandsRepository = brandsRepository;
    private readonly CategoriesRepository _categoriesRepository = categoriesRepository;
    private readonly ProductDetailsRepository _productDetailsRepository = productDetailsRepository;
    private readonly ProductsRepository _productsRepository = productsRepository;
    private readonly ILogger _logger = logger;


    // method: create a new product
    public async Task<bool> CreateProductAsync(ProductRegistrationDto product)
    {
        try
        {
            if (!await _productsRepository.ExistsAsync(x => x.Id == product.ProductId))
            {
                CategoryEntity categoryEntity = await _categoriesRepository.ReadOneAsync(x => x.CategoryName == product.Category);
                categoryEntity ??= await _categoriesRepository.CreateAsync(ProductFactory.Create(product.ParentCategoryId, product.Category));
                ProductEntity productEntity = await _productsRepository.CreateAsync(ProductFactory.Create(product.ProductId, product.ProductName, categoryEntity));
                BrandEntity brandEntity = await _brandsRepository.ReadOneAsync(x => x.BrandName == product.Brand);
                brandEntity ??= await _brandsRepository.CreateAsync(ProductFactory.Create(product.Brand));
                ProductDetailEntity productDetailEntity = await _productDetailsRepository.CreateAsync(ProductFactory.Create(product.ProductId, brandEntity.Id, product.UnitPrice, product.Color, product.Size));
                return true;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductService - CreateProductAsync"); }
        return false;
    }

    // method: read all products
    public async Task<IEnumerable<ProductDto>> ReadAllProductsAsync()
    {
        try
        {
            IEnumerable<ProductEntity> productEntities = await _productsRepository.ReadAllAsync();
            IEnumerable<ProductDto> allProductsDtos = ProductFactory.Create(productEntities);

            return allProductsDtos;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductService - ReadAllProductsAsync"); }
        return null!;
    }

    // method: read one product
    public async Task<ProductDto> ReadOneProductAsync(string id)
    {
        try
        {
            ProductEntity productEntity = await _productsRepository.ReadOneAsync(x => x.Id == id);
            ProductDto productDto = ProductFactory.Create(productEntity);
            return productDto;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductService - ReadOneProductAsync"); }
        return null!;
    }

    // method: update product
    public async Task<ProductDto> UpdateProductAsync(ProductDto product)
    {
        try
        {
            CategoryEntity categoryEntity = await _categoriesRepository.ReadOneAsync(x => x.CategoryName == product.CategoryName);
            categoryEntity ??= await _categoriesRepository.CreateAsync(ProductFactory.Create(product.ParentCategoryId, product.CategoryName));
            ProductEntity productEntity = await _productsRepository.UpdateAsync(x => x.Id == product.ProductId, ProductFactory.Create(product.ProductId, product.ProductName, categoryEntity));

            // BUG!!!!!!!!!!!
            //the correct categoryEntity is added to the productEntity, but I do not succeed in updating the connection table
            //ProductCategories and thus the category on the product is not updated either.

            BrandEntity brandEntity = await _brandsRepository.ReadOneAsync(x => x.BrandName == product.BrandName);
            brandEntity ??= await _brandsRepository.CreateAsync(ProductFactory.Create(product.BrandName));

            ProductDetailEntity productDetailEntity = await _productDetailsRepository.ReadOneAsync(x => x.ProductId == product.ProductId);
            if(productDetailEntity != null)
            {
                productDetailEntity = await _productDetailsRepository.UpdateAsync(x => x.ProductId == product.ProductId, ProductFactory.Create(product.ProductId, brandEntity.Id, product.UnitPrice, product.Color, product.Size));
            }
            else
            {
                productDetailEntity = await _productDetailsRepository.CreateAsync(ProductFactory.Create(product.ProductId, brandEntity.Id, product.UnitPrice, product.Color, product.Size));
            }

            ProductDto productDto = ProductFactory.Create(productEntity, productDetailEntity, brandEntity, categoryEntity);

            return productDto;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductService - UpdateProductAsync"); }
        return null!;
    }

    // method: delete product
    // BUG!!!!!!!!!!!
    // the method does not work because I have not managed to solve how to delete the row that has productId
    // as primary key in the connection table ProductCategories, which needs to be done before you can delete the Product
    public async Task<bool> DeleteProductAsync(ProductDto product)
    {
        try
        {
            if (await _productsRepository.ExistsAsync(x => x.Id == product.ProductId))
            {            
                bool productDetailsResult = await _productDetailsRepository.DeleteAsync(x => x.ProductId == product.ProductId);
                bool productResult = await _productsRepository.DeleteAsync(x => x.Id == product.ProductId);
                if (productDetailsResult && productResult)
                {
                    return true;
                }
                return false;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductService - DeleteProductAsync"); }
        return false;
    }
}



























            //ProductEntity updatedProductEntity = new();
            //CategoryEntity updatedCategoryEntity = new();
            //CategoryEntity existingCategoryEntity = await _categoriesRepository.ReadOneAsync(x => x.CategoryName == product.CategoryName); 
            //if (existingCategoryEntity != null)
            //{
            //    updatedProductEntity = await _productsRepository.UpdateAsync(x => x.Id == product.ProductId, ProductFactory.Create(product.ProductId, product.ProductName, existingCategoryEntity));
            //    updatedCategoryEntity = existingCategoryEntity;
            //}
            //else
            //{
            //    CategoryEntity newCategoryEntity = await _categoriesRepository.CreateAsync(ProductFactory.Create(product.ParentCategoryId, product.CategoryName));
            //    updatedProductEntity = await _productsRepository.UpdateAsync(x => x.Id == product.ProductId, ProductFactory.Create(product.ProductId, product.ProductName, newCategoryEntity));
            //    updatedCategoryEntity = newCategoryEntity;
            //}

            //ProductDto productDto = ProductFactory.Create(updatedProductEntity, productDetailEntity, brandEntity, updatedCategoryEntity);


















            //BrandEntity brandEntity = new();
            //int brandId = product.BrandId;
            //brandEntity = await _brandsRepository.ReadOneAsync(x => x.BrandName == product.BrandName);
            //if (brandEntity != null)
            //{
            //    brandId = brandEntity.Id;
            //}
            //else
            //{
            //    brandEntity = await _brandsRepository.CreateAsync(ProductFactory.Create(product.BrandName));
            //    brandId = brandEntity.Id;
            //}
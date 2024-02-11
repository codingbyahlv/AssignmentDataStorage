using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProductsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : BaseRepository<ProductEntity, ProductCatalogContext>(productCatalogContext, logger), IProductsRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
    private readonly ILogger _logger = logger;

    public async override Task<IEnumerable<ProductEntity>> ReadAllAsync()
    {
        try
        {
            List<ProductEntity> entities = await _productCatalogContext.Products
                .Include(x => x.ProductDetail)
                    .ThenInclude(y => y.Brand) 
                .Include(z => z.Categories)
                .ToListAsync();
            return entities;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductsRepository - OverrideReadAllAsync"); }
        return null!;
    }

    public async override Task<ProductEntity> ReadOneAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {
            ProductEntity? oneEntity = await _productCatalogContext.Products
                .Include(x => x.ProductDetail)
                    .ThenInclude(y => y.Brand) 
                .Include(z => z.Categories)  
                .FirstOrDefaultAsync(predicate);
            return oneEntity ?? null!;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "ProductsRepository - OverrideReadOneAsync"); }
        return null!;
    }
}



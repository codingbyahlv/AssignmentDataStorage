using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class ProductsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : BaseRepository<ProductEntity, ProductCatalogContext>(productCatalogContext, logger), IProductsRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
    private readonly ILogger _logger = logger;
}

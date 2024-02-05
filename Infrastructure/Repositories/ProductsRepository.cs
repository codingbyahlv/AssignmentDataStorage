using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class ProductsRepository : BaseRepository<ProductEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _productCatalogContext;
    private readonly ILogger _logger;

    public ProductsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : base(productCatalogContext, logger)
    {
        _productCatalogContext = productCatalogContext;
        _logger = logger;
    }
}

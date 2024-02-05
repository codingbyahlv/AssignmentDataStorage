using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class ProductDetailsRepository : BaseRepository<ProductDetailEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _productCatalogContext;
    private readonly ILogger _logger;

    public ProductDetailsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : base(productCatalogContext, logger)
    {
        _productCatalogContext = productCatalogContext;
        _logger = logger;
    }
}

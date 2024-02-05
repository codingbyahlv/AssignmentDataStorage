using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class BrandsRepository : BaseRepository<BrandEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _productCatalogContext;
    private readonly ILogger _logger;

    public BrandsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : base(productCatalogContext, logger)
    {
        _productCatalogContext = productCatalogContext;
        _logger = logger;
    }
}

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class BrandsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : BaseRepository<BrandEntity, ProductCatalogContext>(productCatalogContext, logger), IBrandsRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
    private readonly ILogger _logger = logger;
}

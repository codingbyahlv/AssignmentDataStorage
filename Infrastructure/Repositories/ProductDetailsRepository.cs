using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class ProductDetailsRepository(ProductCatalogContext productCatalogContext, ILogger logger) : BaseRepository<ProductDetailEntity, ProductCatalogContext>(productCatalogContext, logger), IProductDetailsRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
    private readonly ILogger _logger = logger;
}

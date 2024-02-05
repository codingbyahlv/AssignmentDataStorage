using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class CategoriesRepository(ProductCatalogContext productCatalogContext, ILogger logger) : BaseRepository<CategoryEntity, ProductCatalogContext>(productCatalogContext, logger), ICategoriesRepository
{
    private readonly ProductCatalogContext _productCatalogContext = productCatalogContext;
    private readonly ILogger _logger = logger;
}

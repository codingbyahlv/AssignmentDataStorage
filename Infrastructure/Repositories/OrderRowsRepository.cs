using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRowsRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : BaseRepository<OrderRowEntity, CustomersOrdersDbContext>(customersOrdersDbContext, logger), IOrderRowsRepository
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext = customersOrdersDbContext;
    private readonly ILogger _logger = logger;
}

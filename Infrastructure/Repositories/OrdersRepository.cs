using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class OrdersRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : BaseRepository<OrderEntity, CustomersOrdersDbContext>(customersOrdersDbContext, logger), IOrdersRepository
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext = customersOrdersDbContext;
    private readonly ILogger _logger = logger;
}

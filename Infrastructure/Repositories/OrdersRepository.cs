using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class OrdersRepository : BaseRepository<OrderEntity, CustomersOrdersDbContext>
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext;
    private readonly ILogger _logger;

    public OrdersRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : base(customersOrdersDbContext, logger)
    {
        _customersOrdersDbContext = customersOrdersDbContext;
        _logger = logger;
    }
}

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class AddressesRepository : BaseRepository<AddressEntity, CustomersOrdersDbContext>
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext;
    private readonly ILogger _logger;

    public AddressesRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : base(customersOrdersDbContext, logger)
    {
        _customersOrdersDbContext = customersOrdersDbContext;
        _logger = logger;
    }
}

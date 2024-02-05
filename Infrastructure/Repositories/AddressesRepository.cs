using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories;

public class AddressesRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : BaseRepository<AddressEntity, CustomersOrdersDbContext>(customersOrdersDbContext, logger), IAddressesRepository
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext = customersOrdersDbContext;
    private readonly ILogger _logger = logger;
}

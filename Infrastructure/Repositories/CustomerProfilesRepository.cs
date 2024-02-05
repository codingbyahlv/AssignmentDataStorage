using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Shared.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerProfilesRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : BaseRepository<CustomerProfileEntity, CustomersOrdersDbContext>(customersOrdersDbContext, logger), ICustomerProfilesRepository
    {
        private readonly CustomersOrdersDbContext _customersOrdersDbContext = customersOrdersDbContext;
        private readonly ILogger _logger = logger;
    }
}



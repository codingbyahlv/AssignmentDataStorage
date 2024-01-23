using Infrastructure.Contexts;
using Infrastructure.Entities;
using Shared.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerProfilesRepository : BaseRepository<CustomerProfileEntity, CustomersOrdersDbContext>
    {
        private readonly CustomersOrdersDbContext _customersOrdersDbContext;
        private readonly ILogger _logger;

        public CustomerProfilesRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : base(customersOrdersDbContext, logger)
        {
            _customersOrdersDbContext = customersOrdersDbContext;
            _logger = logger;
        }
    }
}



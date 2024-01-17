using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class CustomerProfilesRepository(CustomersOrdersDbContext customersOrdersDbContext) : BaseRepository<CustomerProfileEntity>(customersOrdersDbContext)
    {
    }
}



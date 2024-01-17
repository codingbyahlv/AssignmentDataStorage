using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomersRepository(CustomersOrdersDbContext customersOrdersDbContext) : BaseRepository<CustomerEntity>(customersOrdersDbContext)
{
   
}

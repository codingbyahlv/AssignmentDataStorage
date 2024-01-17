using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressesRepository(CustomersOrdersDbContext customersOrdersDbContext) : BaseRepository<AddressEntity>(customersOrdersDbContext)
{
}

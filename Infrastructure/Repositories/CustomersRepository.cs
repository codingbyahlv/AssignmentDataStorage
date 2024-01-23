using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomersRepository : BaseRepository<CustomerEntity, CustomersOrdersDbContext>
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext;
    private readonly ILogger _logger;

    public CustomersRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : base(customersOrdersDbContext, logger)
    {
        _customersOrdersDbContext = customersOrdersDbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<CustomerEntity>> ReadAllWithAllInfoAsync()
    {
        try
        {
            List<CustomerEntity> entities = await _customersOrdersDbContext.Customers
                .Include(x => x.CustomerProfile)
                    .ThenInclude(y => y.Address)
                .ToListAsync();
                return entities ?? null!;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadAllWithAllInfoAsync"); }
        return null!;
    }

    public async Task<CustomerEntity> ReadOneWithAllInfoAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            CustomerEntity? oneEntity = await _customersOrdersDbContext.Customers
                .Include(x => x.CustomerProfile)
                    .ThenInclude(y => y.Address)
                .FirstOrDefaultAsync(predicate);
                return oneEntity ?? null!;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadOneWithAllInfoAsync"); }
        return null!;
    }
}

using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomersRepository(CustomersOrdersDbContext customersOrdersDbContext, ILogger logger) : BaseRepository<CustomerEntity, CustomersOrdersDbContext>(customersOrdersDbContext, logger), ICustomersRepository
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext = customersOrdersDbContext;
    private readonly ILogger _logger = logger;

    public async override Task<IEnumerable<CustomerEntity>> ReadAllAsync()
    {
        try
        {
            List<CustomerEntity> entities = await _customersOrdersDbContext.Customers
                .Include(x => x.CustomerProfile)
                    .ThenInclude(y => y.Address) //?????????
                .ToListAsync();
            return entities;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadAllWithAllInfoAsync"); }
        return null!;
    }

    public async override Task<CustomerEntity> ReadOneAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            CustomerEntity? oneEntity = await _customersOrdersDbContext.Customers
                .Include(x => x.CustomerProfile)
                    .ThenInclude(y => y.Address)   //??????????
                .FirstOrDefaultAsync(predicate);
            return oneEntity ?? null!;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadOneWithAllInfoAsync"); }
        return null!;
    }
}






    //public async Task<IEnumerable<CustomerEntity>> ReadAllWithAllInfoAsync()
    //{
    //    try
    //    {
    //        List<CustomerEntity> entities = await _customersOrdersDbContext.Customers
    //            .Include(x => x.CustomerProfile)
    //                .ThenInclude(y => y.Address) //?????????
    //            .ToListAsync();
    //            return entities;
    //    }
    //    catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadAllWithAllInfoAsync"); }
    //    return null!;
    //}

    //public async Task<CustomerEntity> ReadOneWithAllInfoAsync(Expression<Func<CustomerEntity, bool>> predicate)
    //{
    //    try
    //    {
    //        CustomerEntity? oneEntity = await _customersOrdersDbContext.Customers
    //            .Include(x => x.CustomerProfile)
    //                .ThenInclude(y => y.Address)   //??????????
    //            .FirstOrDefaultAsync(predicate);
    //            return oneEntity ?? null!;
    //    }
    //    catch (Exception ex) { _logger.Log(ex.Message, "CustomerRepository - ReadOneWithAllInfoAsync"); }
    //    return null!;
    //}
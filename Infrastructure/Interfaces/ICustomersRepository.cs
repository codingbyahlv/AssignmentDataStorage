using Infrastructure.Entities;

namespace Infrastructure.Interfaces;

public interface ICustomersRepository : IBaseRepository<CustomerEntity>
{
    //Task<IEnumerable<CustomerEntity>> ReadAllWithAllInfoAsync();
    //Task<CustomerEntity> ReadOneWithAllInfoAsync(Expression<Func<CustomerEntity, bool>> predicate);
}

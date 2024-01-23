﻿using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces;

public interface ICustomerRepository : IBaseRepository<CustomerEntity>
{
    Task<IEnumerable<CustomerEntity>> ReadAllWithAllInfoAsync();
    Task<CustomerEntity> ReadOneWithAllInfoAsync(Expression<Func<CustomerEntity, bool>> predicate);
}
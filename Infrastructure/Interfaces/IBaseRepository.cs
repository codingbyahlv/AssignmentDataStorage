﻿using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> ReadAllAsync();
        Task<TEntity> ReadOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity newEntity);
    }
}
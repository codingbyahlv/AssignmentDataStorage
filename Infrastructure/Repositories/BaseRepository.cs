using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext context;
    private readonly ILogger _logger;

    protected BaseRepository(TContext customersOrdersDbContext, ILogger logger)
    {
        context = customersOrdersDbContext;
        _logger = logger;
    }

    //method: CREATE new entity
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - CreateAsync"); }
        return null!;
    }

    //method: READ(CHECK) if entity exists based on predicate
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            bool found = await context.Set<TEntity>().AnyAsync(predicate);
            return found;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - ExistsAsync"); }
        return false;
    }

    //method: READ all entities
    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        try
        {
            List<TEntity> entityList = await context.Set<TEntity>().ToListAsync();
            return entityList;
        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - ReadAllAsync"); }
        return null!;
    }


    //method: READ entity based on predicate
    public virtual async Task<TEntity> ReadOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            TEntity? oneEntity = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

            if (oneEntity != null)
            {
                return oneEntity;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - ReadOneAsync"); }
        return null!;
    }


    //method: UPDATE entity based on predicate
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity newEntity)
    {
        try
        {
            TEntity? currentEntity = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

            if (currentEntity != null && newEntity != null)
            {
                context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
                await context.SaveChangesAsync();

                return currentEntity;
            }

        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - UpdateAsync"); }
        return null!;
    }


    //method: DELETE entity based on predicate
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            TEntity? currentEntity = await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

            if (currentEntity != null)
            {
                context.Set<TEntity>().Remove(currentEntity);
                await context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex) { _logger.Log(ex.Message, "BaseRepository - DeleteAsync"); }
        return false;
    }
}   

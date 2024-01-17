using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    private readonly CustomersOrdersDbContext _customersOrdersDbContext;

    protected BaseRepository(CustomersOrdersDbContext customersOrdersDbContext)
    {
        _customersOrdersDbContext = customersOrdersDbContext;
    }

    //CREATE
    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _customersOrdersDbContext.Set<TEntity>().Add(entity);
            _customersOrdersDbContext.SaveChanges();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
        return null!;
    }


    //READ - all
    public virtual IEnumerable<TEntity> ReadAll()
    {
        try
        {
            var entityList = _customersOrdersDbContext.Set<TEntity>().ToList();
            return entityList;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
        return null!;
    }


    //READ - one
    public virtual TEntity ReadOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var oneEntity = _customersOrdersDbContext.Set<TEntity>().FirstOrDefault(predicate);

            if (oneEntity != null)
            {
                return oneEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
        return null!;
    }


    //UPDATE
    public virtual TEntity Update()
    {
        try
        {


        }
        catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
        return null!;
    }


    //DELETE
    public virtual bool Delete()
    {
        try
        {


        }
        catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
        return false;
    }




}   



        //try
        //{


        //}
        //catch (Exception ex) { Debug.WriteLine("ERROR MESSAGE: " + ex.Message); }
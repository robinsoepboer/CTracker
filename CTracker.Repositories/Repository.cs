using CTracker.DAL;
using CTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CTracker.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected CTrackerContext Context;
    protected DbSet<T> DbSet;
    
    public Repository(CTrackerContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
    
    public virtual IQueryable<T> All()
    {
        return DbSet.Where(x => !x.IsDeleted);
    }
    
    public virtual T? Get(int id)
    {
        return All().FirstOrDefault(t => t.Id == id);
    }

    public virtual void Insert(T entity)
    {
        DbSet.Add(entity);
    }

    public void Commit()
    {
        Context.SaveChanges();
    }
}
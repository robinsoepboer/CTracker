using CTracker.DAL;
using CTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CTracker.Repositories;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> All();
    T? Get(int id);
    void Insert(T entity);
    void Commit();
}
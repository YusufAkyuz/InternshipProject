using System.Linq.Expressions;
using Emp.Core.Entities;
using Emp.Data.Context;
using Emp.Data.Repositories.Contracts;
using Emp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emp.Data.Repositories.Concretes;

public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<T> Table
    {
        get => _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        _dbContext.SaveChanges();
    }

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null, 
        int pageNumber = 1, 
        int pageSize = 10, 
        params Expression<Func<T, object>>[]? includedProperties)
    {
        IQueryable<T> query = Table;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (includedProperties != null && includedProperties.Any())
        {
            foreach (var item in includedProperties)
            {
                query = query.Include(item);
            }
        }

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }


    public async Task<T> GetByGuidAsync(Guid id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => Table.Update(entity));
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
        _dbContext.SaveChanges();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return await Table.CountAsync(predicate);
    }

    public async Task<IEnumerable<User>> SearchAsync(string name, string lastName)
    {
        IQueryable<User> query = _dbContext.Users;
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName))
        {
            query = query.Where(e => e.Name.Contains(name) || e.LastName.Contains(lastName));
        }

        return await query.ToListAsync();
    }
}
using System.Globalization;
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

    public string ToUpperTurkish(string input)
    {
        return input.ToUpper(new CultureInfo("tr-TR", false));
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
        var users = await query.ToListAsync();

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName))
        {
            var normalizedName = ToUpperTurkish(name);
            var normalizedLastName = ToUpperTurkish(lastName);
            users = users.Where(e =>
            {
                var nameDb = e.Name;
                var lastNameDb = e.LastName;
                var normalizedNameDb = ToUpperTurkish(nameDb);
                var normalizedLastNameDb = ToUpperTurkish(lastNameDb);
                return normalizedNameDb.Contains(normalizedName) && normalizedLastNameDb.Contains(normalizedLastName);
            }).ToList();
        }
    
        else if (!string.IsNullOrEmpty(name))
        {
            var normalizedName = ToUpperTurkish(name);
            users = users.Where(e =>
            {
                var lastNameDB = e.Name;
                var normalizedNameDb = ToUpperTurkish(lastNameDB);
                return normalizedNameDb.Contains(normalizedName);
            }).ToList();
        }

        else if (!string.IsNullOrEmpty(lastName))
        {
            var normalizedLastName = ToUpperTurkish(lastName);
            users = users.Where(e =>
            {
                var lastNameDB = e.LastName;
                var normalizedLastNameDb = ToUpperTurkish(lastNameDB);
                return normalizedLastNameDb.Contains(normalizedLastName);
            }).ToList();
        }

        else
        {
            return _dbContext.Users;
        }

        return users;
    }
    
}
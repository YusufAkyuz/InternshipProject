using Emp.Core.Entities;
using Emp.Data.Context;
using Emp.Data.Repositories.Concretes;
using Emp.Data.Repositories.Contracts;

namespace Emp.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        dbContext = appDbContext;
    }

    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntityBase, new()    //IEntityBase'den türeyen bir class gelsin
    {
        return new Repository<T>(dbContext);
    }

    public async Task<int> SaveAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public int Save()
    {
        return dbContext.SaveChanges();
    }
}
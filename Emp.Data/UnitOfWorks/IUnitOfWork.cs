using Emp.Core.Entities;
using Emp.Data.Repositories.Contracts;

namespace Emp.Data.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
    Task<int> SaveAsync();
    int Save();
}
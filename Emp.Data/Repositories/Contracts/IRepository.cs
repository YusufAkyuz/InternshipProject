using System.Linq.Expressions;
using Emp.Core.Entities;

namespace Emp.Data.Repositories.Contracts;

public interface IRepository<T> where T : class, IEntityBase, new()
{
    Task AddAsync(T entity);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        params Expression<Func<T, Object>>[] includedProperties);

    Task<T> GetByGuidAsync(Guid id);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);              //Belli koşulu sağlayanın kendisi
    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);  //Belli koşulu sağlayanların sayısı

}
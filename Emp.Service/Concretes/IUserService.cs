using System.Linq.Expressions;
using Emp.Entity.Entities;

namespace Emp.Service.Concretes;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize);
    Task<User> GetUserById(Guid userId);
    Task AddAsync(User entity);
    Task<User> UpdateAsync(User entity);
    Task DeleteAsync(User entity);
    Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);              //Belli koşulu sağlayanın kendisi
    Task<int> CountAsync(Expression<Func<User, bool>> predicate = null);  //Belli koşulu sağlayanların sayısı
    Task<IEnumerable<User>> SearchAsync(string name, string lastName);
}
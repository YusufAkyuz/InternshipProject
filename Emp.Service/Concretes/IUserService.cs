using Emp.Entity.Entities;

namespace Emp.Service.Concretes;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize);
    Task<User> GetUserById(Guid userId);
}
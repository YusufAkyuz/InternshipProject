using Emp.Entity.Entities;

namespace Emp.Service.Concretes;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserById(Guid userId);
}
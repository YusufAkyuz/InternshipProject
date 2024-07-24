using System.Linq.Expressions;
using Emp.Data.UnitOfWorks;
using Emp.Entity.Entities;
using Emp.Service.Concretes;

namespace Emp.Service.Contracts;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return await _unitOfWork.GetRepository<User>().GetAllAsync(null, pageNumber, pageSize, null);
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await _unitOfWork.GetRepository<User>().GetByGuidAsync(userId);
    }

    public Task AddAsync(User entity)
    {
        return _unitOfWork.GetRepository<User>().AddAsync(entity);
    }

    public async Task<User> UpdateAsync(User entity)
    {
        await _unitOfWork.GetRepository<User>().UpdateAsync(entity);
        return entity;
    }

    public Task DeleteAsync(User entity)
    {
        return _unitOfWork.GetRepository<User>().DeleteAsync(entity);
    }

    public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
    {
        return await _unitOfWork.GetRepository<User>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<User, bool>> predicate = null)
    {
        return await _unitOfWork.GetRepository<User>().CountAsync(predicate);
    }

    public Task<IEnumerable<User>> SearchAsync(string name, string lastName)
    {
        return _unitOfWork.GetRepository<User>().SearchAsync(name, lastName);
    }
}
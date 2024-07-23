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
}
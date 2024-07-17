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
    public Task<List<User>> GetAllUsersAsync()
    {
        return _unitOfWork.GetRepository<User>().GetAllAsync();
    }
}
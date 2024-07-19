using AutoMapper;
using Emp.Entity.DTOs;
using Emp.Entity.Entities;

namespace Emp.Service.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<UserAddDto, User>().ReverseMap();
    }
}
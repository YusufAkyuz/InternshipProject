using Emp.Entity.Entities;
using Emp.Service.Concretes;
using Emp.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Emp.Service.Extensions;

public static class ServiceExtension
{
    public static void UserServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}
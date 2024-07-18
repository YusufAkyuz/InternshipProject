using Emp.Data.Context;
using Emp.Data.Repositories.Concretes;
using Emp.Data.Repositories.Contracts;
using Emp.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emp.Data.Extensions;

public static class DataLayerExtensions
{
    public static IServiceCollection DbContextExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
    public static IServiceCollection RepositoryExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
    public static IServiceCollection UnitOfWorkExtension(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        return services;
    }
}
using System.Globalization;
using System.Reflection;
using Emp.Service.Concretes;
using Emp.Service.Contracts;
using Emp.Service.FluentValidations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Emp.Service.Extensions;

public static class ServiceExtension
{
    public static void UserServiceExtension(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(assembly);

        services.AddControllersWithViews().AddFluentValidation(opt =>
        {
            opt.RegisterValidatorsFromAssemblyContaining<UserValidator>();
            opt.DisableDataAnnotationsValidation = true;
            opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
        });
    }
}
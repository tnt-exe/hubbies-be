using Hubbies.Application.Features.Accounts;
using Hubbies.Application.Features.Auths;

namespace Hubbies.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthRepository>();
        services.AddScoped<IAccountService, AccountRepository>();



        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
namespace Hubbies.Web.Configuration;

public static class AuthorizationConfigure
{
    public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.Admin, policy => policy.RequireRole(Role.Admin))
            .AddPolicy(Policy.EventHost, policy => policy.RequireRole(Role.EventHost))
            .AddPolicy(Policy.Customer, policy => policy.RequireRole(Role.Customer));

        return services;
    }
}

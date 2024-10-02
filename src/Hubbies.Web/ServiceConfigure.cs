using Hubbies.Web.Service;

namespace Hubbies.Web;

public static class ServiceConfigure
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUser, UserAccessor>();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddSwaggerDoc(configuration);

        services.AddJwtAuth();

        services.AddAuthorizationPolicy();

        services.AddCorsServices();

        services.AddRateLimiterServices(configuration);

        services.AddHealthChecksServices(configuration);

        return services;
    }
}

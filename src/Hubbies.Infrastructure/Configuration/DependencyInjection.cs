using Hubbies.Infrastructure.Auth;
using Hubbies.Infrastructure.Identity;
using Hubbies.Infrastructure.Persistence;
using Hubbies.Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hubbies.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((service, options) =>
        {
            options.AddInterceptors(service.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(configuration.GetConnectionString("HUBBIES_DB")!);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddTransient<IJwtService, JwtService>();

        services.Configure<FirebaseSettings>(configuration.GetSection("FirebaseSettings"));
        services.AddSingleton<IFirebaseService, FirebaseService>();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(
                name: "Hubbies EF Core DbContext",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "sql", "postgre", "efcore"])
            .AddNpgSql(
                configuration.GetConnectionString("HUBBIES_DB")!,
                name: "Hubbies Database",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["db", "sql", "postgre"]);

        return services;
    }
}

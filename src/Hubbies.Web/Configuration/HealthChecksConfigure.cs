using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Hubbies.Web.Configuration;

public static class HealthChecksConfigure
{
    public static IServiceCollection AddHealthChecksServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serverUrl = configuration["SERVER_URL"] ?? "";

        services.AddHealthChecksUI(options =>
        {
            options.AddHealthCheckEndpoint("Hubbies API", $"{serverUrl}/api/healthz");
        })
        .AddInMemoryStorage();

        return services;
    }

    public static void UseHealthChecksServices(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/api/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(options =>
        {
            options.UIPath = "/healthchecks";
            options.PageTitle = "Hubbies Health Checks UI";
        });
    }
}

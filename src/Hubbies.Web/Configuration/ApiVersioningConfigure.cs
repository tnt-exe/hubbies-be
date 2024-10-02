using Asp.Versioning;

namespace Hubbies.Web.Configuration;

public static class ApiVersioningConfigure
{
    public static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;

            options.AssumeDefaultVersionWhenUnspecified = true;

            options.DefaultApiVersion = new ApiVersion(1, 0);

            options.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("api-version"),
                new QueryStringApiVersionReader("api-version"),
                new MediaTypeApiVersionReader("version"));

        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";

            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}

using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hubbies.Web.Configuration;

public static class SwaggerConfigure
{
    public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IConfiguration configuration)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Hubbies.WebApi",
                Description = $"Hubbies WebApi version 1 ({environment})",
                Version = "v1",
                License = new OpenApiLicense
                {
                    Name = "GNU General Public License v3.0",
                    Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html")
                }
            });

            var controllerXmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var controllerXmlPath = Path.Combine(AppContext.BaseDirectory, controllerXmlFile);

            config.IncludeXmlComments(controllerXmlPath);

            var dtoXmlFile = Assembly.GetAssembly(typeof(BaseRepository))!.GetName().Name + ".xml";
            var dtoXmlPath = Path.Combine(AppContext.BaseDirectory, dtoXmlFile);

            config.IncludeXmlComments(dtoXmlPath);

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            config.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            var swaggerServers = configuration.GetSection("SwaggerServers").Get<List<OpenApiServer>>();
            if (swaggerServers != null && environment != "Development")
            {
                foreach (var server in swaggerServers)
                {
                    config.AddServer(server);
                }
            }

        })
        .AddFluentValidationRulesToSwagger();

        return services;
    }
}

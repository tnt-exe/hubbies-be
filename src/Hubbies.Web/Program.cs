using Hubbies.Infrastructure.Configuration;
using System.Text.Json.Serialization;

namespace Hubbies.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(
                new KebabCaseParameterTransformer()));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddWebServices(builder.Configuration);

        var app = builder.Build();

        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRateLimiter();

        app.UseHealthChecksServices();

        app.UseCors();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireRateLimiting(RateLimiterConfigure.BucketLimiter);

        app.Run();
    }
}

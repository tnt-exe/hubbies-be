using Microsoft.AspNetCore.RateLimiting;

namespace Hubbies.Web.Configuration;
public static class RateLimiterConfigure
{
    public const string BucketLimiter = "bucket";
    public static IServiceCollection AddRateLimiterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddTokenBucketLimiter(BucketLimiter, options =>
            {
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(configuration.GetValue<int>("RateLimitSettings:Bucket:ReplenishmentPeriod"));
                options.TokenLimit = configuration.GetValue<int>("RateLimitSettings:Bucket:TokenLimit");
                options.TokensPerPeriod = configuration.GetValue<int>("RateLimitSettings:Bucket:TokensPerPeriod");
            });
        });

        return services;
    }
}

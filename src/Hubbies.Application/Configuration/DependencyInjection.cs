using Hubbies.Application.Features.Accounts;
using Hubbies.Application.Features.Auths;
using Hubbies.Application.Features.EventCategories;
using Hubbies.Application.Features.Feedbacks;
using Hubbies.Application.Features.Notifications;
using Hubbies.Application.Features.Orders;
using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthRepository>();
        services.AddScoped<IAccountService, AccountRepository>();

        services.AddScoped<IEventCategoryService, EventCategoryRepository>();
        services.AddScoped<IFeedbackService, FeedbackRepository>();
        services.AddScoped<ITicketEventService, TicketEventRepository>();
        services.AddScoped<IOrderService, OrderRepository>();
        services.AddScoped<INotificationService, NotificationRepository>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

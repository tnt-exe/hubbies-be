namespace Hubbies.Application.Features.Notifications;

public record NotificationQueryParameter
{
    public string? Title { get; init; }
    public string? From { get; init; }
}

public static class NotificationQueryExtensions
{
    public static IQueryable<Notification> Filter(this IQueryable<Notification> query, NotificationQueryParameter parameter)
    {
        if (parameter.Title is not null)
        {
            query = query.Where(x => x.Title!.Contains(parameter.Title));
        }

        if (parameter.From is not null)
        {
            query = query.Where(x => x.From!.Contains(parameter.From));
        }

        return query;
    }
}

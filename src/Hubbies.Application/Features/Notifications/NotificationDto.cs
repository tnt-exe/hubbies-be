namespace Hubbies.Application.Features.Notifications;

public record NotificationDto
{
    public Guid Id { get; init; }

    /// <example>System Announcement</example>
    public string? Title { get; init; }

    /// <example>System now returns to monke</example>
    public string? Content { get; init; }
    public bool IsRead { get; init; }

    /// <example>Monke Admin</example>
    public string? From { get; init; }

    public DateTimeOffset? SentAt { get; init; }
}

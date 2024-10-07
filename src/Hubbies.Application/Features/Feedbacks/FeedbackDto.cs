namespace Hubbies.Application.Features.Feedbacks;

public record FeedbackDto
{
    public Guid TicketEventId { get; init; }
    public Guid UserId { get; init; }

    /// <example>Event full of monkes, love it</example>
    public string? Content { get; init; }

    /// <example>5</example>
    public int Rating { get; init; }
    public FeedbackStatus Status { get; init; }
}

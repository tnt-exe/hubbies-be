namespace Hubbies.Application.Features.Feedbacks;

public record FeedbackDto
{
    public Guid TicketEventId { get; init; }
    public Guid UserId { get; init; }
    public string? Content { get; init; }
    public int Rating { get; init; }
    public FeedbackStatus Status { get; init; }
}

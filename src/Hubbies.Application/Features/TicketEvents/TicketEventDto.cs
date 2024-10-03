using Hubbies.Application.Features.EventCategories;
using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Application.Features.TicketEvents;

public record TicketEventDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public TicketEventStatus Status { get; init; }
    public TicketApprovalStatus ApprovalStatus { get; init; }
    public string? Address { get; init; }
    public DateTimeOffset PostDate { get; init; }
    public string? Image { get; init; }
    public Guid EventCategoryId { get; init; }

    public EventCategoryDto? EventCategory { get; init; }
    public IEnumerable<FeedbackDto> Feedbacks { get; init; } = default!;
}

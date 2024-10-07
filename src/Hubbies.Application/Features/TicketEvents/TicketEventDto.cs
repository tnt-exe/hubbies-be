using Hubbies.Application.Features.EventCategories;
using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Application.Features.TicketEvents;

public record TicketEventDto
{
    public Guid Id { get; init; }

    /// <example>Cofi event</example>
    public string? Name { get; init; }

    /// <example>Event full of monkes doing art</example>
    public string? Description { get; init; }

    /// <example>50</example>
    public int Quantity { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }
    public TicketEventStatus Status { get; init; }
    public TicketApprovalStatus ApprovalStatus { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; init; }

    /// <example>2024-10-29</example>
    public DateTimeOffset PostDate { get; init; }

    /// <example>https://monke-cofi.com/image.jpg</example>
    public string? Image { get; init; }
    public Guid EventCategoryId { get; init; }

    public EventCategoryDto? EventCategory { get; init; }
    public IEnumerable<FeedbackDto> Feedbacks { get; init; } = default!;
}

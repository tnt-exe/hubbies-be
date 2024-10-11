using Hubbies.Domain.Enums;

namespace Hubbies.Domain.Models;

public class TicketEvent : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public TicketEventStatus Status { get; set; }
    public TicketApprovalStatus ApprovalStatus { get; set; }
    public string? Address { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public string? Image { get; set; }
    public Guid EventCategoryId { get; set; }
    public Guid EventHostId { get; set; }

    public EventCategory? EventCategory { get; set; }
    public ApplicationUser? EventHost { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; } = default!;
}

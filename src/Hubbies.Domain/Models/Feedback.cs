using Hubbies.Domain.Enums;

namespace Hubbies.Domain.Models;

public class Feedback : BaseAuditableEntity
{
    public Guid TicketEventId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public int Rating { get; set; }
    public FeedbackStatus Status { get; set; }

    public TicketEvent? TicketEvent { get; set; }
    public ApplicationUser? User { get; set; }
}

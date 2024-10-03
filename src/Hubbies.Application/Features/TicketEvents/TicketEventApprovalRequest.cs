namespace Hubbies.Application.Features.TicketEvents;

public record TicketEventApprovalRequest
{
    public Guid TicketEventId { get; init; }
    public TicketApprovalStatus ApprovalStatus { get; init; }
}

public class TicketEventApprovalRequestValidator : AbstractValidator<TicketEventApprovalRequest>
{
    public TicketEventApprovalRequestValidator()
    {
        RuleFor(x => x.TicketEventId)
            .NotEmpty();

        RuleFor(x => x.ApprovalStatus.ToString())
            .IsEnumName(typeof(TicketApprovalStatus));
    }
}
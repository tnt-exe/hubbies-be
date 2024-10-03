namespace Hubbies.Application.Features.Feedbacks;

public record FeedbackApprovalRequest
{
    public Guid UserId { get; init; }
    public Guid TicketEventId { get; init; }
    public FeedbackStatus ApprovalStatus { get; init; }
}

public class FeedbackApprovalRequestValidator : AbstractValidator<FeedbackApprovalRequest>
{
    public FeedbackApprovalRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.TicketEventId)
            .NotEmpty();

        RuleFor(x => x.ApprovalStatus)
            .IsInEnum()
            .WithMessage("Approval Status is invalid.");
    }
}

namespace Hubbies.Application.Features.Feedbacks;

public record UpdateFeedbackRequest
{
    public Guid TicketEventId { get; init; }
    public string? Content { get; init; }
    public int Rating { get; init; }
}

public class UpdateFeedbackRequestValidator : AbstractValidator<UpdateFeedbackRequest>
{
    public UpdateFeedbackRequestValidator()
    {
        RuleFor(x => x.TicketEventId)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Rating)
            .NotEmpty()
            .InclusiveBetween(1, 5)
            .WithMessage("{PropertyName} must be between 1 and 5.");
    }
}

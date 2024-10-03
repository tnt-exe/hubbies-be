namespace Hubbies.Application.Features.Feedbacks;

public record CreateFeedbackRequest
{
    public Guid TicketEventId { get; init; }
    public string? Content { get; init; }
    public int Rating { get; init; }
}

public class CreateFeedbackRequestValidator : AbstractValidator<CreateFeedbackRequest>
{
    public CreateFeedbackRequestValidator()
    {
        RuleFor(x => x.TicketEventId)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Rating)
            .NotEmpty()
            .InclusiveBetween(1, 5);
    }
}

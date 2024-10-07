namespace Hubbies.Application.Features.Feedbacks;

public record UpdateFeedbackRequest
{
    /// <example>Event full of monkes, love it</example>
    public string? Content { get; init; }

    /// <example>5</example>
    public int Rating { get; init; }
}

public class UpdateFeedbackRequestValidator : AbstractValidator<UpdateFeedbackRequest>
{
    public UpdateFeedbackRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Rating)
            .NotEmpty()
            .InclusiveBetween(1, 5);
    }
}

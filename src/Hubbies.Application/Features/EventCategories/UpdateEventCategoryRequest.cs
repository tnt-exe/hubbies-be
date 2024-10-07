namespace Hubbies.Application.Features.EventCategories;

public record UpdateEventCategoryRequest
{
    /// <example>HCM</example>
    public string? Address { get; init; }

    /// <example>Cofi</example>
    public string? Name { get; init; }
}

public class UpdateEventCategoryRequestValidator : AbstractValidator<UpdateEventCategoryRequest>
{
    public UpdateEventCategoryRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}

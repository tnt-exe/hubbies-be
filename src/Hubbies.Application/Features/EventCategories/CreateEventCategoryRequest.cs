namespace Hubbies.Application.Features.EventCategories;

public record CreateEventCategoryRequest
{
    public string? Address { get; init; }
    public string? Name { get; init; }
}

public class CreateEventCategoryRequestValidator : AbstractValidator<CreateEventCategoryRequest>
{
    public CreateEventCategoryRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}

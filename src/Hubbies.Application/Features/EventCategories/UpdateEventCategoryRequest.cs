namespace Hubbies.Application.Features.EventCategories;

public record UpdateEventCategoryRequest
{
    public string? Address { get; init; }
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

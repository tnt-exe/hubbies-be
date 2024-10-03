namespace Hubbies.Application.Features.TicketEvents;

public record CreateTicketEventRequest
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public string? Address { get; init; }
    public DateTimeOffset PostDate { get; init; }
    public string? Image { get; init; }
    public Guid EventCategoryId { get; init; }
}

public class CreateTicketEventRequestValidator : AbstractValidator<CreateTicketEventRequest>
{
    public CreateTicketEventRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PostDate)
            .NotEmpty();

        RuleFor(x => x.Image)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.EventCategoryId)
            .NotEmpty();
    }
}

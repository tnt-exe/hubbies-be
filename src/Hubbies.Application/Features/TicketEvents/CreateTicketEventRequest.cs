namespace Hubbies.Application.Features.TicketEvents;

public record CreateTicketEventRequest
{
    /// <example>Cofi event</example>
    public string? Name { get; init; }

    /// <example>Event full of monkes doing art</example>
    public string? Description { get; init; }

    /// <example>50</example>
    public int Quantity { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; init; }

    public DateTimeOffset PostDate { get; init; }

    /// <example>https://monke-cofi.com/image.jpg</example>
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

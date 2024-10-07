namespace Hubbies.Application.Features.OrderDetails;

public record CreateOrderDetailsRequest
{
    public Guid TicketEventId { get; init; }

    /// <example>HCM</example>
    public string? Location { get; init; }

    /// <example>2024-10-29</example>
    public DateTimeOffset PreferredTime { get; init; }

    /// <example>5</example>
    public int Quantity { get; init; }
}

public class CreateOrderDetailsRequestValidator : AbstractValidator<CreateOrderDetailsRequest>
{
    public CreateOrderDetailsRequestValidator()
    {
        RuleFor(x => x.TicketEventId)
            .NotEmpty();

        RuleFor(x => x.Location)
            .NotEmpty();

        RuleFor(x => x.PreferredTime)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}

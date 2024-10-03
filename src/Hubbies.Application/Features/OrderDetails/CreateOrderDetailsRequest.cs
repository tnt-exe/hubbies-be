namespace Hubbies.Application.Features.OrderDetails;

public record CreateOrderDetailsRequest
{
    public Guid TicketEventId { get; init; }
    public string? Location { get; init; }
    public DateTimeOffset PreferredTime { get; init; }
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

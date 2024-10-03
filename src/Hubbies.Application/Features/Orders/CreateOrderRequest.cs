using Hubbies.Application.Features.OrderDetails;

namespace Hubbies.Application.Features.Orders;

public record CreateOrderRequest
{
    public int Quantity { get; init; }
    public string? Address { get; init; }

    public IEnumerable<CreateOrderDetailsRequest> OrderDetails { get; init; } = default!;
}

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.OrderDetails)
            .ForEach(x => x.SetValidator(new CreateOrderDetailsRequestValidator()));
    }
}

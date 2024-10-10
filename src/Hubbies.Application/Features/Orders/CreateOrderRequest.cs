using Hubbies.Application.Features.OrderDetails;

namespace Hubbies.Application.Features.Orders;

public record CreateOrderRequest
{
    /// <example>Q9, HCM</example>
    public string? Address { get; init; }

    public IEnumerable<CreateOrderDetailsRequest> OrderDetails { get; init; } = default!;
}

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.OrderDetails)
            .ForEach(x => x.SetValidator(new CreateOrderDetailsRequestValidator()));
    }
}

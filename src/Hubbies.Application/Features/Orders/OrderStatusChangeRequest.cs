namespace Hubbies.Application.Features.Orders;

public record OrderStatusChangeRequest
{
    public Guid OrderId { get; init; }
    public OrderStatus Status { get; init; }
}

public class OrderStatusChangeRequestValidator : AbstractValidator<OrderStatusChangeRequest>
{
    public OrderStatusChangeRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid order status.");
    }
}

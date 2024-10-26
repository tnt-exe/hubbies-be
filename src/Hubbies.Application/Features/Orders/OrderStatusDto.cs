namespace Hubbies.Application.Features.Orders;

public record OrderStatusDto
{
    public Guid OrderId { get; init; }
    public OrderStatus Status { get; init; }
}

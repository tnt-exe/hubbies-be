using Hubbies.Application.Features.OrderDetails;

namespace Hubbies.Application.Features.Orders;

public record OrderDto
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
    public decimal TotalPrice { get; init; }
    public OrderStatus Status { get; init; }
    public string? Address { get; init; }
    public Guid UserId { get; init; }

    public IEnumerable<OrderDetailsDto> OrderDetails { get; init; } = default!;
}

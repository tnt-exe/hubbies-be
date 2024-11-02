namespace Hubbies.Application.Features.Orders;

public record OrderStatusDto
{
    public Guid OrderId { get; init; }
    public OrderStatus Status { get; init; }

    //todo: prop for inspect test
    public string? Data { get; set; }
    public string? DataTs { get; set; }
    public string? Signature { get; set; }
    public string? TestSignature { get; set; }
}

namespace Hubbies.Application.Features.OrderDetails;

public record OrderDetailsDto
{
    public Guid OrderId { get; init; }
    public Guid TicketEventId { get; init; }
    public string? Location { get; init; }
    public DateTimeOffset PreferredTime { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}

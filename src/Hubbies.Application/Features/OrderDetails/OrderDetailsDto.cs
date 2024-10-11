namespace Hubbies.Application.Features.OrderDetails;

public record OrderDetailsDto
{
    public Guid OrderId { get; init; }
    public Guid TicketEventId { get; init; }

    /// <example>HCM</example>
    public string? Location { get; init; }

    public DateTimeOffset PreferredTime { get; init; }

    /// <example>5</example>
    public int Quantity { get; init; }

    /// <example>100000</example>
    public decimal Price { get; init; }
}

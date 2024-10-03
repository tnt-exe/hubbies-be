namespace Hubbies.Domain.Models;

public class OrderDetails : BaseAuditableEntity
{
    public Guid OrderId { get; set; }
    public Guid TicketEventId { get; set; }
    public string? Location { get; set; }
    public DateTimeOffset PreferredTime { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Order? Order { get; set; }
    public TicketEvent? TicketEvent { get; set; }
}

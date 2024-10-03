namespace Hubbies.Domain.Models;

public class EventCategory : IBaseEntity
{
    public Guid Id { get; set; }
    public string? Address { get; set; }
    public string? Name { get; set; }

    public ICollection<TicketEvent> TicketEvents { get; set; } = default!;
}

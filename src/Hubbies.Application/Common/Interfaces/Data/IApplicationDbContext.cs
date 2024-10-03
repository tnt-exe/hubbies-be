namespace Hubbies.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<EventCategory> EventCategories { get; set; }
    DbSet<Feedback> Feedbacks { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderDetails> OrderDetails { get; set; }
    DbSet<TicketEvent> TicketEvents { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

namespace Hubbies.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<EventCategory> EventCategories { get; set; }
    DbSet<Feedback> Feedbacks { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<OrderDetails> OrderDetails { get; set; }
    DbSet<TicketEvent> TicketEvents { get; set; }
    DbSet<Notification> Notifications { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

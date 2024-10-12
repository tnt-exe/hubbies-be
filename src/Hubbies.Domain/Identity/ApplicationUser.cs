namespace Hubbies.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public DateTimeOffset? Dob { get; set; }
    public string? Avatar { get; set; }
    public int LockoutCount { get; set; }
    public ICollection<Order> Orders { get; set; } = default!;
    public ICollection<Feedback> Feedbacks { get; set; } = default!;
    public ICollection<TicketEvent> TicketEvents { get; set; } = default!;
    public ICollection<Notification> Notifications { get; set; } = default!;
}

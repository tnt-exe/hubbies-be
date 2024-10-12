namespace Hubbies.Domain.Models;

public class Notification : IBaseEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool IsRead { get; set; }
    public string? From { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
}

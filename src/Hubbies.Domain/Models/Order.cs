using Hubbies.Domain.Enums;

namespace Hubbies.Domain.Models;

public class Order : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public string? Address { get; set; }
    public Guid UserId { get; set; }

    public ApplicationUser? User { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; } = default!;
    public ICollection<Payment> Payments { get; set; } = default!;
}

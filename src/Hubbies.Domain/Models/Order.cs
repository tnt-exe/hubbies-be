using Hubbies.Domain.Enums;

namespace Hubbies.Domain.Models;

public class Order : BaseAuditableEntity, IBaseEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public string? Address { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; } = default!;
}

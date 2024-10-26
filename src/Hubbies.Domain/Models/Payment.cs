namespace Hubbies.Domain.Models;

public class Payment
{
    public string? PaymentReference { get; set; }
    public string? PaymentType { get; set; }
    public Guid OrderId { get; set; }

    public Order? Order { get; set; }
}

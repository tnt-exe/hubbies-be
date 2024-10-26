namespace Hubbies.Application.Features.Orders;

public record OrderPaymentResponse
{
    public string? PaymentUrl { get; init; }
    public string? PaymentReference { get; init; }
}

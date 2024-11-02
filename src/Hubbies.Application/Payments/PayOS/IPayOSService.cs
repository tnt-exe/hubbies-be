namespace Hubbies.Application.Payments.PayOS;

public interface IPayOSService
{
    Task<(string? paymentUrl, string paymentReference)> GetPaymentUrlAsync(long amount, string description);
    Task<OrderStatus> VerifyPaymentAsync(string paymentRef);
}

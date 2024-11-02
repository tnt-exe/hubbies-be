namespace Hubbies.Application.Payments.PayOS;

public interface IPayOSService
{
    Task<(string? paymentUrl, string paymentReference)> GetPaymentUrlAsync(long amount, string description);
    Task<(OrderStatus, string data, string dataTs, string sig, string testSig)> VerifyPaymentAsync(string paymentRef);
}

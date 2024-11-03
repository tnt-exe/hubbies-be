namespace Hubbies.Application.Payments;

public interface IPaymentService
{
    /// <summary>
    /// Get payment url for payment
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="description"></param>
    /// <returns>
    /// Tuple of payment url and payment reference id
    /// </returns>
    Task<(string? paymentUrl, string paymentId)> GetPaymentUrlAsync(long amount, string description);

    /// <summary>
    /// Verify payment status
    /// </summary>
    /// <param name="paymentId"></param>
    Task<OrderStatus> VerifyPaymentAsync(string paymentId);

    /// <summary>
    /// Callback payment for payment provider
    /// </summary>
    /// <remarks>
    /// Some provider will not have this method implemented.
    /// Thus will be mark with <see cref="ObsoleteAttribute"/>. 
    /// </remarks>
    /// <param name="callbackData"></param>
    dynamic CallbackPayment(dynamic callbackData);
}

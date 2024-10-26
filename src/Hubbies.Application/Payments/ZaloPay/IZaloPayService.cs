namespace Hubbies.Application.Payments.ZaloPay;

public interface IZaloPayService
{
    /// <summary>
    /// Get ZaloPay payment url
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="description"></param>
    /// <returns>
    /// paymentUrl: ZaloPay payment url
    /// appTransId: ZaloPay transaction id, use as reference when verify payment
    /// </returns>
    Task<(string? paymentUrl, string appTransId)> GetPaymentUrl(long amount, string description);

    /// <summary>
    /// Verify payment status
    /// </summary>
    /// <param name="appTransId">ZaloPay transaction id</param>
    /// <returns>Order status</returns>
    Task<OrderStatus> VerifyPayment(string appTransId);

    /// <summary>
    /// Callback payment
    /// </summary>
    /// <param name="callbackData">Callback data sent from ZaloPay</param>
    /// <returns>
    /// Dictionary of key-value pairs.
    /// This will return to ZaloPay server to confirm the callback.
    /// </returns>
    Dictionary<string, object> CallbackPayment(dynamic callbackData);
}

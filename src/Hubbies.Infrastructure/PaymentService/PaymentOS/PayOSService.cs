using Hubbies.Application.Features.Payments;
using Net.payOS;
using Net.payOS.Types;

namespace Hubbies.Infrastructure.PaymentService.PaymentOS;

public class PayOSService(IOptions<PayOSConfiguration> configuration, PayOS payOS)
    : IPaymentService
{
    private readonly PayOSConfiguration _configuration = configuration.Value;

    [Obsolete("This method is not implemented for PayOS", true)]
    public dynamic CallbackPayment(dynamic callbackData)
    {
        throw new NotImplementedException();
    }

    public async Task<(string? paymentUrl, string paymentId)> GetPaymentUrlAsync(long amount, string description)
    {
        var provider = "?provider=" + PaymentProvider.PayOS;

        var paymentData = new PaymentData(
            orderCode: new Random().Next(100000000, 999999999),
            amount: (int)amount,
            description: description,
            items: [],
            cancelUrl: _configuration.CancelUrl! + provider,
            returnUrl: _configuration.ReturnUrl! + provider
        );

        var createPaymentResult = await payOS.createPaymentLink(paymentData);

        return (createPaymentResult.checkoutUrl, createPaymentResult.orderCode.ToString());
    }

    public async Task<OrderStatus> VerifyPaymentAsync(string paymentId)
    {
        var paymentLinkInformation = await payOS.getPaymentLinkInformation(long.Parse(paymentId));

        return paymentLinkInformation.status switch
        {
            "PENDING" => OrderStatus.Pending,
            "PROCESSING" => OrderStatus.Pending,
            "PAID" => OrderStatus.Finished,
            "CANCELLED" => OrderStatus.Canceled,
            _ => OrderStatus.Canceled
        };
    }
}

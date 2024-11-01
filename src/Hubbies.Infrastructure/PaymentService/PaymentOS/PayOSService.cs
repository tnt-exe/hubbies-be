using Hubbies.Application.Payments;
using Hubbies.Application.Payments.PayOS;
using Net.payOS;
using Net.payOS.Types;

namespace Hubbies.Infrastructure.PaymentService.PaymentOS;

public class PayOSService(IOptions<PayOSConfiguration> configuration, PayOS payOS)
    : IPayOSService
{
    private readonly PayOSConfiguration _configuration = configuration.Value;

    public async Task<(string? paymentUrl, string paymentReference)> GetPaymentUrlAsync(long amount, string description)
    {
        var provider = "?provider=" + PaymentType.PayOS;

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

    public async Task<OrderStatus> VerifyPaymentAsync(string paymentRef)
    {
        var paymentLinkInformation = await payOS.getPaymentLinkInformation(long.Parse(paymentRef));

        var orderResult = paymentLinkInformation.status;

        return orderResult switch
        {
            "PAID" => OrderStatus.Finished,
            "PENDING" => OrderStatus.Pending,
            "CANCELLED" => OrderStatus.Canceled,
            _ => OrderStatus.Canceled
        };
    }
}

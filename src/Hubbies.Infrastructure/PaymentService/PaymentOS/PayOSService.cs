using Hubbies.Application.Payments;
using Hubbies.Application.Payments.PayOS;
using Microsoft.Extensions.Logging;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json.Linq;

namespace Hubbies.Infrastructure.PaymentService.PaymentOS;

public class PayOSService(IOptions<PayOSConfiguration> configuration, PayOS payOS, ILogger<PayOSService> logger)
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
        // var paymentLinkInformation = await payOS.getPaymentLinkInformation(long.Parse(paymentRef));

        string requestUri = "https://api-merchant.payos.vn/v2/payment-requests/" + paymentRef;
        JObject jObject = JObject.Parse(await (await new HttpClient().SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri)
        {
            Headers =
            {
                { "x-client-id", _configuration.ClientId },
                { "x-api-key", _configuration.ApiKey }
            }
        })).Content.ReadAsStringAsync());

        string data = jObject["data"]!.ToString();
        string signature = jObject["signature"]!.ToString();

        logger.LogInformation("Data: " + data);
        logger.LogInformation("Data ts: " + data.ToString());

        logger.LogInformation("Signature: " + signature);
        Console.WriteLine("Signature: " + signature);

        var testSignature = SignatureControlTest.CreateSignatureFromObj(jObject, _configuration.ChecksumKey!);
        logger.LogInformation("Test Signature: " + testSignature);

        var orderResult = "CANCELLED"; //default value to test

        return orderResult switch
        {
            "PAID" => OrderStatus.Finished,
            "PENDING" => OrderStatus.Pending,
            "CANCELLED" => OrderStatus.Canceled,
            _ => OrderStatus.Canceled
        };
    }
}

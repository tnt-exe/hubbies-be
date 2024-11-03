using Hubbies.Application.Payments;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(
    [FromKeyedServices(nameof(PaymentProvider.ZaloPay))] IPaymentService zaloPayService)
    : ControllerBase
{
    /// <summary>
    /// ZaloPay callback, called by ZaloPay server after payment is made
    /// </summary>
    /// <param name="callbackData"></param>
    [HttpPost("zalopay/callback", Name = "ZaloPayCallback")]
    public IActionResult ZaloPayCallback([FromBody] dynamic callbackData)
    {
        var result = zaloPayService.CallbackPayment(callbackData);
        return Ok(result);
    }
}

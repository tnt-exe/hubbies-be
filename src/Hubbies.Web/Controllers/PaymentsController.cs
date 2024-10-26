using Hubbies.Application.Payments.ZaloPay;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(
    IZaloPayService zaloPayService)
    : ControllerBase
{
    /// <summary>
    /// ZaloPay callback, called by ZaloPay server after payment is made
    /// </summary>
    /// <param name="callbackData"></param>
    [HttpPost("zalopay/callback", Name = "ZaloPayCallback")]
    public async Task<IActionResult> ZaloPayCallback([FromBody] dynamic callbackData)
    {
        var result = await zaloPayService.CallbackPayment(callbackData);
        return Ok(result);
    }
}

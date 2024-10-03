using Hubbies.Application.Features.Orders;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class OrdersController(IOrderService orderService)
    : ControllerBase
{
    [HttpGet(Name = "GetOrders")]
    [ProducesResponseType(typeof(PaginationResponse<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderQueryParameter queryParameter,
        [FromQuery] OrderIncludeParameter includeParameter)
    {
        var response = await orderService.GetOrdersAsync(queryParameter, includeParameter);

        return Ok(response);
    }

    [HttpGet("{orderId}", Name = "GetOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderAsync(Guid orderId, [FromQuery] OrderIncludeParameter includeParameter)
    {
        var response = await orderService.GetOrderAsync(orderId, includeParameter);

        return Ok(response);
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
    {
        var response = await orderService.CreateOrderAsync(request);

        return CreatedAtRoute("GetOrder", new { orderId = response.Id }, response);
    }

    [HttpPut("status", Name = "UpdateOrderStatus")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderStatusAsync([FromQuery] OrderStatusChangeRequest orderStatus)
    {
        await orderService.OrderStatusChangeAsync(orderStatus);

        return NoContent();
    }
}

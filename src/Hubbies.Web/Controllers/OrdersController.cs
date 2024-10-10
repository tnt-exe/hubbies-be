using Hubbies.Application.Features.Orders;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class OrdersController(IOrderService orderService)
    : ControllerBase
{
    /// <summary>
    /// Get orders with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Returns orders</response>
    [HttpGet(Name = "GetOrders")]
    [ProducesResponseType(typeof(PaginationResponse<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] OrderQueryParameter queryParameter,
        [FromQuery] OrderIncludeParameter includeParameter)
    {
        var response = await orderService.GetOrdersAsync(queryParameter, includeParameter);

        return Ok(response);
    }

    /// <summary>
    /// Get order by id
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Returns order</response>
    /// <response code="404">Order not found</response>
    [HttpGet("{orderId:guid}", Name = "GetOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderAsync(Guid orderId, [FromQuery] OrderIncludeParameter includeParameter)
    {
        var response = await orderService.GetOrderAsync(orderId, includeParameter);

        return Ok(response);
    }

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/orders
    ///     {
    ///         "address": "HCM",
    ///         "orderDetails": [
    ///             {
    ///                 "ticketEventId": "00000000-0000-0000-0000-000000000000",
    ///                 "location": "Q1 HCM",
    ///                 "preferredTime": "2024-09-29",
    ///                 "quantity": 1
    ///             }
    ///         ]
    ///      }
    ///     
    /// </remarks>
    /// <response code="201">Order created</response>
    /// <response code="404">Ticket event not found</response>
    /// <response code="422">Unprocessable entity</response>
    [Authorize(Policy.Customer)]
    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
    {
        var response = await orderService.CreateOrderAsync(request);

        return CreatedAtRoute("GetOrder", new { orderId = response.Id }, response);
    }

    /// <summary>
    /// Update order status
    /// </summary>
    /// <param name="orderStatus"></param>
    /// <response code="204">Order status updated</response>
    /// <response code="404">Order not found</response>
    [Authorize]
    [HttpPut("status", Name = "UpdateOrderStatus")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderStatusAsync([FromQuery] OrderStatusChangeRequest orderStatus)
    {
        await orderService.OrderStatusChangeAsync(orderStatus);

        return NoContent();
    }
}

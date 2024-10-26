using Hubbies.Application.Features.Orders;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IOrderService
{
    /// <summary>
    /// Get orders with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <returns>
    /// The task result contains a <see cref="PaginationResponse{OrderDto}"/> object.
    /// </returns>
    Task<PaginationResponse<OrderDto>> GetOrdersAsync(
        OrderQueryParameter queryParameter, OrderIncludeParameter includeParameter);

    /// <summary>
    /// Get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeParameter"></param>
    /// <returns>
    /// The task result contains an <see cref="OrderDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Order not found</exception>
    Task<OrderDto> GetOrderAsync(Guid id, OrderIncludeParameter includeParameter);

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains an <see cref="OrderPaymentResponse"/> object.
    /// </returns>
    /// <exception cref="ValidationException">Validation error</exception>
    /// <exception cref="BadRequestException">Failed to create order for various reasons</exception>
    /// <exception cref="NotFoundException">Ticket event not found</exception>
    Task<OrderPaymentResponse> CreateOrderAsync(CreateOrderRequest request);

    /// <summary>
    /// Check order status
    /// </summary>
    /// <param name="paymentReference"></param>
    /// <param name="paymentType"></param>
    /// <returns>
    /// The task result contains an <see cref="OrderStatusDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Payment or order not found</exception>
    Task<OrderStatusDto> CheckOrderStatus(string paymentReference, string paymentType);
}

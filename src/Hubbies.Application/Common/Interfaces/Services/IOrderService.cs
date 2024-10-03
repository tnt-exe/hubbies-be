using Hubbies.Application.Features.Orders;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IOrderService
{
    Task<PaginationResponse<OrderDto>> GetOrdersAsync(
        OrderQueryParameter queryParameter, OrderIncludeParameter includeParameter);

    Task<OrderDto> GetOrderAsync(Guid id, OrderIncludeParameter includeParameter);

    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request);

    Task OrderStatusChangeAsync(Guid id, OrderStatus status);
}

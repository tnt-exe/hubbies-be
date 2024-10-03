namespace Hubbies.Application.Features.Orders;

public class OrderRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), IOrderService
{
    public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
    {
        await ValidateAsync(request);

        var order = Mapper.Map<Order>(request);

        // get list of ticket events
        // validate if all ticket events are found
        var ticketEventIds = request.OrderDetails.Select(x => x.TicketEventId).ToList();
        var ticketEvents = await Context.TicketEvents
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Where(x => ticketEventIds.Contains(x.Id))
            .ToListAsync();

        // exception will be thrown on ids that are not found
        if (ticketEvents.Count != ticketEventIds.Count)
        {
            var notFoundIds = ticketEventIds
                .Except(ticketEvents.Select(x => x.Id))
                .ToList();

            throw new NotFoundException(nameof(TicketEvent), notFoundIds);
        }

        foreach (var item in request.OrderDetails)
        {
            var ticketEvent = ticketEvents
                .Single(x => x.Id == item.TicketEventId);

            // calculate total price
            order.TotalPrice += ticketEvent.Price * item.Quantity;

            // append price to order details
            var orderDetail = order.OrderDetails
                .Single(x => x.TicketEventId == item.TicketEventId);
            orderDetail.Price = ticketEvent.Price;
        }

        order.UserId = Guid.Parse(user.Id!);
        order.Status = OrderStatus.Pending;

        await Context.Orders.AddAsync(order);
        await Context.SaveChangesAsync();

        return Mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> GetOrderAsync(Guid id, OrderIncludeParameter includeParameter)
    {
        var order = await Context.Orders
            .AsNoTracking()
            .Include(includeParameter)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Order), id);

        return Mapper.Map<OrderDto>(order);
    }

    public async Task<PaginationResponse<OrderDto>> GetOrdersAsync(
        OrderQueryParameter queryParameter, OrderIncludeParameter includeParameter)
    {
        await ValidateAsync(queryParameter);

        var orders = await Context.Orders
            .AsNoTracking()
            .Filter(queryParameter)
            .Include(includeParameter)
            .ToPaginationResponseAsync<Order, OrderDto>(queryParameter, Mapper);

        return orders;
    }

    public async Task OrderStatusChangeAsync(Guid id, OrderStatus status)
    {
        var order = await Context.Orders
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(Order), id);

        order.Status = status;

        await Context.SaveChangesAsync();
    }
}

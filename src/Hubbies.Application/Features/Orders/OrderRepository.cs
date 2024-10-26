using Hubbies.Application.Payments;
using Hubbies.Application.Payments.ZaloPay;

namespace Hubbies.Application.Features.Orders;

public class OrderRepository(
    IApplicationDbContext context,
    IMapper mapper,
    IServiceProvider serviceProvider,
    IUser user,
    IZaloPayService zaloPayService)
    : BaseRepository(context, mapper, serviceProvider), IOrderService
{
    public async Task<OrderPaymentResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        await ValidateAsync(request);

        var order = Mapper.Map<Order>(request);

        // get list of ticket events
        // validate if all ticket events are found
        var ticketEventIds = request.OrderDetails.Select(x => x.TicketEventId).ToList();
        var ticketEvents = await Context.TicketEvents
            .Where(x => x.IsDeleted == false)
            .Where(x => ticketEventIds.Contains(x.Id))
            .ToListAsync();

        // exception will be thrown on ids that are not found
        if (ticketEvents.Count != ticketEventIds.Count)
        {
            var notFoundIds = ticketEventIds
                .Except(ticketEvents.Select(x => x.Id))
                .ToList();

            throw new NotFoundException(nameof(TicketEvent), string.Join(", ", notFoundIds));
        }

        // check each ticket if quantity is enough
        foreach (var item in request.OrderDetails)
        {
            var ticketEvent = ticketEvents
                .Single(x => x.Id == item.TicketEventId);

            if (ticketEvent.Quantity < item.Quantity)
            {
                throw new BadRequestException(
                    $"Unsufficient quantity ({ticketEvent.Quantity} left) for ticket event {ticketEvent.Id}");
            }
        }

        foreach (var item in request.OrderDetails)
        {
            var ticketEvent = ticketEvents
                .Single(x => x.Id == item.TicketEventId);

            // calculate total price
            order.TotalPrice += ticketEvent.Price * item.Quantity;

            // subtract quantity from ticket event
            ticketEvent.Quantity -= item.Quantity;

            // append price to order details
            var orderDetail = order.OrderDetails
                .Single(x => x.TicketEventId == item.TicketEventId);
            orderDetail.Price = ticketEvent.Price;
        }

        order.UserId = Guid.Parse(user.Id!);
        order.Status = OrderStatus.Pending;

        await Context.Orders.AddAsync(order);

        var paymentReference = new Payment()
        {
            OrderId = order.Id
        };

        string? orderPaymentUrl;
        string? orderPaymentReference;
        var orderDescription = $"Thanh toán đơn hàng {order.Id}";
        if (request.PaymentType == PaymentType.ZaloPay)
        {
            (string? paymentUrl, string appTransId) = await zaloPayService.GetPaymentUrl((long)order.TotalPrice, orderDescription);

            if (paymentUrl is null)
            {
                throw new BadRequestException("Failed to create payment url");
            }

            paymentReference.PaymentReference = appTransId;
            paymentReference.PaymentType = PaymentType.ZaloPay.ToString();

            orderPaymentUrl = paymentUrl;
            orderPaymentReference = appTransId;
        }
        else
        {
            //todo: add other payments, or forget about it
            orderPaymentReference = "";
            orderPaymentUrl = "";
        }

        await Context.Payments.AddAsync(paymentReference);

        await Context.SaveChangesAsync();

        return new OrderPaymentResponse()
        {
            PaymentUrl = orderPaymentUrl,
            PaymentReference = orderPaymentReference
        };
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

    public async Task<OrderStatusDto> CheckOrderStatus(string paymentReference, string paymentType)
    {
        var payment = await Context.Payments
            .FirstOrDefaultAsync(x => x.PaymentReference == paymentReference)
            ?? throw new NotFoundException(nameof(Payment), paymentReference);

        var order = await Context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.TicketEvent)
            .ThenInclude(x => x!.EventHost)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == payment.OrderId)
            ?? throw new NotFoundException(nameof(Order), payment.OrderId);

        OrderStatus orderStatus;
        if (paymentType == PaymentType.ZaloPay.ToString())
        {
            orderStatus = await zaloPayService.VerifyPayment(paymentReference);
        }
        else
        {
            orderStatus = OrderStatus.Canceled;
        }

        order.Status = orderStatus;

        if (orderStatus != OrderStatus.Finished)
        {
            return new OrderStatusDto()
            {
                OrderId = order.Id,
                Status = order.Status
            };
        }

        static Notification CreateNotification(Guid userId, string orderId)
        {
            return new Notification()
            {
                UserId = userId,
                Title = $"Đơn hàng đã hoàn thành #{DateTimeOffset.Now.ToLocalTime()}",
                Content = $"Đơn hàng {orderId} đã được thanh toán thành công",
                From = "System"
            };
        }

        var adminId = Guid.Parse("c71da9be-4110-4452-89b8-2982c76efc1f");

        var userNotification = CreateNotification(order.UserId, order.Id.ToString());
        var adminNotification = CreateNotification(adminId, order.Id.ToString());
        List<Notification> eventHostNotifications = [];

        userNotification.UserId = order.UserId;
        adminNotification.UserId = adminId;

        foreach (var eventHost in order.OrderDetails.Select(x => x.TicketEvent!.EventHost))
        {
            var eventHostNotification = CreateNotification(eventHost!.Id, order.Id.ToString());
            eventHostNotification.UserId = eventHost!.Id;
            eventHostNotifications.Add(eventHostNotification);
        }

        await Context.Notifications.AddRangeAsync([userNotification, adminNotification]);
        await Context.Notifications.AddRangeAsync(eventHostNotifications);

        await Context.SaveChangesAsync();

        return new OrderStatusDto()
        {
            OrderId = order.Id,
            Status = order.Status
        };
    }
}

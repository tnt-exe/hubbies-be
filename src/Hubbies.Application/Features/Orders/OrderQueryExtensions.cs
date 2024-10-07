namespace Hubbies.Application.Features.Orders;

public record OrderQueryParameter : PaginationQueryParameter
{
    public Guid UserId { get; set; }
    public string? Address { get; set; }
    public OrderStatus? Status { get; set; }
}

public record OrderIncludeParameter
{
    /// <example>true</example>
    public bool IncludeOrderDetails { get; set; }
}

public class OrderQueryParameterValidator : AbstractValidator<OrderQueryParameter>
{
    public OrderQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());
    }
}

public static class OrderQueryExtensions
{
    public static IQueryable<Order> Filter(this IQueryable<Order> query, OrderQueryParameter queryParameter)
    {
        if (queryParameter.UserId != Guid.Empty)
        {
            query = query.Where(x => x.UserId == queryParameter.UserId);
        }

        if (queryParameter.Address is not null)
        {
            query = query.Where(x => x.Address!.Contains(queryParameter.Address));
        }

        if (queryParameter.Status is not null)
        {
            query = query.Where(x => x.Status == queryParameter.Status);
        }

        return query;
    }

    public static IQueryable<Order> Include(this IQueryable<Order> query, OrderIncludeParameter includeParameter)
    {
        if (includeParameter.IncludeOrderDetails)
        {
            query = query.Include(x => x.OrderDetails);
        }

        return query;
    }
}

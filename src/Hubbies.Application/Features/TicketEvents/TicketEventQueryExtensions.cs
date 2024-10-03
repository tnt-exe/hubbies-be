namespace Hubbies.Application.Features.TicketEvents;

public record TicketEventQueryParameter : PaginationQueryParameter
{
    public string? Name { get; init; }
    public string? Address { get; init; }
}

public record TicketEventIncludeParameter
{
    public bool IncludeEventCategory { get; init; }
    public bool IncludeFeedbacks { get; init; }
}

public class TicketEventQueryParameterValidator : AbstractValidator<TicketEventQueryParameter>
{
    public TicketEventQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());
    }
}

public static class TicketEventQueryExtensions
{
    public static IQueryable<TicketEvent> Filter(this IQueryable<TicketEvent> query, TicketEventQueryParameter parameter)
    {
        if (parameter.Name is not null)
        {
            query = query.Where(x => x.Name!.Contains(parameter.Name));
        }

        if (parameter.Address is not null)
        {
            query = query.Where(x => x.Address!.Contains(parameter.Address));
        }

        return query;
    }

    public static IQueryable<TicketEvent> Include(this IQueryable<TicketEvent> query, TicketEventIncludeParameter parameter)
    {
        if (parameter.IncludeEventCategory)
        {
            query = query.Include(x => x.EventCategory);
        }

        if (parameter.IncludeFeedbacks)
        {
            query = query.Include(x => x.Feedbacks);
        }

        return query;
    }
}

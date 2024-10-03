namespace Hubbies.Application.Features.Feedbacks;

public record FeedbackQueryParameter : PaginationQueryParameter
{
    public Guid TicketEventId { get; init; }
    public Guid UserId { get; init; }
    public int? Rating { get; set; }
}

public class FeedbackQueryParameterValidator : AbstractValidator<FeedbackQueryParameter>
{
    public FeedbackQueryParameterValidator()
    {
        Include(new PaginationQueryParameterValidator());

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
    }
}

public static class FeedbackQueryExtensions
{
    public static IQueryable<Feedback> Filter(this IQueryable<Feedback> query, FeedbackQueryParameter parameter)
    {
        if (parameter.TicketEventId != Guid.Empty)
        {
            query = query.Where(x => x.TicketEventId == parameter.TicketEventId);
        }

        if (parameter.UserId != Guid.Empty)
        {
            query = query.Where(x => x.UserId == parameter.UserId);
        }

        if (parameter.Rating > 0)
        {
            query = query.Where(x => x.Rating == parameter.Rating);
        }

        return query;
    }
}

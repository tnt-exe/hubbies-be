namespace Hubbies.Application.Features.Feedbacks;

public class FeedbackRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider, IUser user)
    : BaseRepository(context, mapper, serviceProvider), IFeedbackService
{
    public async Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackRequest request)
    {
        await ValidateAsync(request);

        var feedback = Mapper.Map<Feedback>(request);

        feedback.UserId = Guid.Parse(user.Id!);

        await Context.Feedbacks.AddAsync(feedback);

        await Context.SaveChangesAsync();

        return Mapper.Map<FeedbackDto>(feedback);
    }

    public async Task DeleteFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        var feedback = await Context.Feedbacks
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Status == FeedbackStatus.Approved)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.TicketEventId == ticketEventId)
            ?? throw new NotFoundException(nameof(Feedback), new { UserId = userId, TicketEventId = ticketEventId });

        Context.Feedbacks.Remove(feedback);

        await Context.SaveChangesAsync();
    }

    public async Task FeedbackApprovalAsync(FeedbackApprovalRequest approvalRequest)
    {
        await ValidateAsync(approvalRequest);

        var feedback = await Context.Feedbacks
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.UserId == approvalRequest.UserId
                                && x.TicketEventId == approvalRequest.TicketEventId)
            ?? throw new NotFoundException(
                nameof(Feedback),
                new { approvalRequest.UserId, approvalRequest.TicketEventId });

        feedback.Status = approvalRequest.ApprovalStatus;

        await Context.SaveChangesAsync();
    }

    public async Task<FeedbackDto> GetFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        var feedback = await Context.Feedbacks
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Status == FeedbackStatus.Approved)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.TicketEventId == ticketEventId)
            ?? throw new NotFoundException(nameof(Feedback), new { UserId = userId, TicketEventId = ticketEventId });

        return Mapper.Map<FeedbackDto>(feedback);
    }

    public async Task<PaginationResponse<FeedbackDto>> GetFeedbacksAsync(FeedbackQueryParameter queryParameter)
    {
        await ValidateAsync(queryParameter);

        var feedbacks = await Context.Feedbacks
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Status == FeedbackStatus.Approved)
            .Filter(queryParameter)
            .ToPaginationResponseAsync<Feedback, FeedbackDto>(queryParameter, Mapper);

        return feedbacks;
    }

    public async Task<FeedbackDto> UpdateFeedbackAsync(Guid ticketEventId, UpdateFeedbackRequest request)
    {
        await ValidateAsync(request);

        var userId = Guid.Parse(user.Id!);

        var feedback = await Context.Feedbacks
            .Where(x => x.IsDeleted == false)
            .Where(x => x.Status == FeedbackStatus.Approved)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.TicketEventId == ticketEventId)
            ?? throw new NotFoundException(nameof(Feedback), new { UserId = userId, TicketEventId = ticketEventId });

        Mapper.Map(request, feedback);

        await Context.SaveChangesAsync();

        return Mapper.Map<FeedbackDto>(feedback);
    }
}

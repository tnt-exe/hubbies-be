namespace Hubbies.Application.Features.TicketEvents;

public class TicketEventRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ITicketEventService
{
    public async Task TicketEventApprovalAsync(TicketEventApprovalRequest approvalRequest)
    {
        await ValidateAsync(approvalRequest);

        var ticketEvent = await Context.TicketEvents
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == approvalRequest.TicketEventId)
            ?? throw new NotFoundException(nameof(TicketEvent), approvalRequest.TicketEventId);

        ticketEvent.ApprovalStatus = approvalRequest.ApprovalStatus;

        await Context.SaveChangesAsync();
    }

    public async Task<TicketEventDto> CreateTicketEventAsync(CreateTicketEventRequest request)
    {
        await ValidateAsync(request);

        var ticketEvent = Mapper.Map<TicketEvent>(request);

        ticketEvent.Status = TicketEventStatus.Available;
        ticketEvent.ApprovalStatus = TicketApprovalStatus.Pending;

        await Context.TicketEvents.AddAsync(ticketEvent);

        await Context.SaveChangesAsync();

        return Mapper.Map<TicketEventDto>(ticketEvent);
    }

    public async Task DeleteTicketEventAsync(Guid id)
    {
        var ticketEvent = await Context.TicketEvents
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(TicketEvent), id);

        Context.TicketEvents.Remove(ticketEvent);

        await Context.SaveChangesAsync();
    }

    public async Task<TicketEventDto> GetTicketEventAsync(Guid id, TicketEventIncludeParameter includeParameter)
    {
        var ticketEvent = await Context.TicketEvents
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Include(includeParameter)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(TicketEvent), id);

        return Mapper.Map<TicketEventDto>(ticketEvent);
    }

    public async Task<PaginationResponse<TicketEventDto>> GetTicketEventsAsync(
        TicketEventQueryParameter queryParameter, TicketEventIncludeParameter includeParameter)
    {
        await ValidateAsync(queryParameter);

        var ticketEvents = await Context.TicketEvents
            .AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .Filter(queryParameter)
            .Include(includeParameter)
            .ToPaginationResponseAsync<TicketEvent, TicketEventDto>(queryParameter, Mapper);

        return ticketEvents;
    }

    public async Task<TicketEventDto> UpdateTicketEventAsync(Guid id, UpdateTicketEventRequest request)
    {
        await ValidateAsync(request);

        var ticketEvent = await Context.TicketEvents
            .Where(x => x.IsDeleted == false)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(TicketEvent), id);

        if (request.Quantity != ticketEvent.Quantity)
        {
            ticketEvent.Status = request.Quantity > 0
                ? TicketEventStatus.Available
                : TicketEventStatus.SoldOut;
        }

        Mapper.Map(request, ticketEvent);

        await Context.SaveChangesAsync();

        return Mapper.Map<TicketEventDto>(ticketEvent);
    }
}

using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface ITicketEventService
{
    Task<PaginationResponse<TicketEventDto>> GetTicketEventsAsync(
        TicketEventQueryParameter queryParameter, TicketEventIncludeParameter includeParameter);

    Task<TicketEventDto> GetTicketEventAsync(Guid id, TicketEventIncludeParameter includeParameter);

    Task<TicketEventDto> CreateTicketEventAsync(CreateTicketEventRequest request);

    Task<TicketEventDto> UpdateTicketEventAsync(Guid id, UpdateTicketEventRequest request);

    Task DeleteTicketEventAsync(Guid id);

    Task TicketEventApprovalAsync(Guid id, TicketApprovalStatus approvalStatus);
}

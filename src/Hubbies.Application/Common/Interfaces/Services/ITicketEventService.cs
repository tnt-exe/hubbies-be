using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface ITicketEventService
{
    /// <summary>
    /// Get ticket events with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <returns>
    /// The task result contains a <see cref="PaginationResponse{TicketEventDto}"/> object.
    /// </returns>
    Task<PaginationResponse<TicketEventDto>> GetTicketEventsAsync(
        TicketEventQueryParameter queryParameter, TicketEventIncludeParameter includeParameter);

    /// <summary>
    /// Get ticket event by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeParameter"></param>
    /// <returns>
    /// The task result contains an <see cref="TicketEventDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Ticket event not found</exception>
    Task<TicketEventDto> GetTicketEventAsync(Guid id, TicketEventIncludeParameter includeParameter);

    /// <summary>
    /// Create ticket event
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains an <see cref="TicketEventDto"/> object.
    /// </returns>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<TicketEventDto> CreateTicketEventAsync(CreateTicketEventRequest request);

    /// <summary>
    /// Update ticket event
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains an <see cref="TicketEventDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Ticket event not found</exception>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<TicketEventDto> UpdateTicketEventAsync(Guid id, UpdateTicketEventRequest request);

    /// <summary>
    /// Delete ticket event
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException">Ticket event not found</exception>
    Task DeleteTicketEventAsync(Guid id);

    /// <summary>
    /// Ticket event approval
    /// </summary>
    /// <param name="approvalRequest"></param>
    /// <exception cref="NotFoundException">
    /// Ticket event is deleted or not having 'Pending' status
    /// </exception>
    Task TicketEventApprovalAsync(TicketEventApprovalRequest approvalRequest);
}

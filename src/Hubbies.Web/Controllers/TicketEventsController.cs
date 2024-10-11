using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class TicketEventsController(ITicketEventService ticketEventService)
    : ControllerBase
{
    /// <summary>
    /// Get ticket events with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Return ticket events with pagination</response>
    [HttpGet(Name = "GetTicketEvents")]
    [ProducesResponseType(typeof(PaginationResponse<TicketEventDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTicketEventsAsync(
        [FromQuery] TicketEventQueryParameter queryParameter,
        [FromQuery] TicketEventIncludeParameter includeParameter)
    {
        var response = await ticketEventService.GetTicketEventsAsync(queryParameter, includeParameter);

        return Ok(response);
    }

    /// <summary>
    /// Get ticket event by id
    /// </summary>
    /// <param name="ticketEventId"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Return ticket event</response>
    /// <response code="404">Ticket event not found</response>
    [HttpGet("{ticketEventId:guid}", Name = "GetTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketEventAsync(
        Guid ticketEventId,
        [FromQuery] TicketEventIncludeParameter includeParameter)
    {
        var response = await ticketEventService.GetTicketEventAsync(ticketEventId, includeParameter);

        return Ok(response);
    }

    /// <summary>
    /// Create ticket event
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/ticket-events
    ///     {
    ///         "name": "Cofi",
    ///         "description": "Cofi description",
    ///         "quantity": 100,
    ///         "price": 100000,
    ///         "address": "HCM",
    ///         "postDate": "2024-10-10",
    ///         "image": "https://monke-cofi.com/image.jpg",
    ///         "eventCategoryId": "00000000-0000-0000-0000-000000000000"
    ///     }
    ///    
    /// </remarks>
    /// <response code="201">Return created ticket event</response>
    /// <response code="422">Validation failed</response>
    [Authorize(Policy.EventHost)]
    [HttpPost(Name = "CreateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateTicketEventAsync(CreateTicketEventRequest request)
    {
        var response = await ticketEventService.CreateTicketEventAsync(request);

        return CreatedAtRoute("GetTicketEvent", new { ticketEventId = response.Id }, response);
    }

    /// <summary>
    /// Update ticket event
    /// </summary>
    /// <param name="ticketEventId"></param>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/ticket-events/{ticketEventId}
    ///     {
    ///         "name": "Cofi",
    ///         "description": "Cofi description",
    ///         "quantity": 100,
    ///         "price": 100000,
    ///         "address": "HCM",
    ///         "postDate": "2024-10-10",
    ///         "image": "https://monke-cofi.com/image.jpg",
    ///         "eventCategoryId": "00000000-0000-0000-0000-000000000000"
    ///     }
    ///    
    /// </remarks>
    /// <response code="200">Return updated ticket event</response>
    /// <response code="404">Ticket event not found</response>
    /// <response code="422">Validation failed</response>
    [Authorize(Policy.EventHost)]
    [HttpPut("{ticketEventId:guid}", Name = "UpdateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateTicketEventAsync(Guid ticketEventId, UpdateTicketEventRequest request)
    {
        var response = await ticketEventService.UpdateTicketEventAsync(ticketEventId, request);

        return Ok(response);
    }

    /// <summary>
    /// Delete ticket event
    /// </summary>
    /// <param name="ticketEventId"></param>
    /// <response code="204">Ticket event deleted</response>
    /// <response code="404">Ticket event not found</response>
    [Authorize(Policy.EventHost)]
    [HttpDelete("{ticketEventId:guid}", Name = "DeleteTicketEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicketEventAsync(Guid ticketEventId)
    {
        await ticketEventService.DeleteTicketEventAsync(ticketEventId);

        return NoContent();
    }

    /// <summary>
    /// Ticket event approval
    /// </summary>
    /// <param name="approvalRequest"></param>
    /// <response code="204">Ticket event approved</response>
    /// <response code="404">Ticket event is deleted or not 'Pending' status</response>
    [Authorize(Policy.Admin)]
    [HttpPut("approval", Name = "TicketEventApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> TicketEventApprovalAsync([FromQuery] TicketEventApprovalRequest approvalRequest)
    {
        await ticketEventService.TicketEventApprovalAsync(approvalRequest);

        return NoContent();
    }
}

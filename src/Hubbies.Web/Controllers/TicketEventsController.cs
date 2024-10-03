using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class TicketEventsController(ITicketEventService ticketEventService)
    : ControllerBase
{
    [HttpGet(Name = "GetTicketEvents")]
    [ProducesResponseType(typeof(PaginationResponse<TicketEventDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTicketEventsAsync(
        [FromQuery] TicketEventQueryParameter queryParameter,
        [FromQuery] TicketEventIncludeParameter includeParameter)
    {
        var response = await ticketEventService.GetTicketEventsAsync(queryParameter, includeParameter);

        return Ok(response);
    }

    [HttpGet("{ticketEventId}", Name = "GetTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketEventAsync(
        Guid ticketEventId,
        [FromQuery] TicketEventIncludeParameter includeParameter)
    {
        var response = await ticketEventService.GetTicketEventAsync(ticketEventId, includeParameter);

        return Ok(response);
    }

    [HttpPost(Name = "CreateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateTicketEventAsync(CreateTicketEventRequest request)
    {
        var response = await ticketEventService.CreateTicketEventAsync(request);

        return CreatedAtRoute("GetTicketEvent", new { ticketEventId = response.Id }, response);
    }

    [HttpPut("{ticketEventId}", Name = "UpdateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateTicketEventAsync(Guid ticketEventId, UpdateTicketEventRequest request)
    {
        var response = await ticketEventService.UpdateTicketEventAsync(ticketEventId, request);

        return Ok(response);
    }

    [HttpDelete("{ticketEventId}", Name = "DeleteTicketEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicketEventAsync(Guid ticketEventId)
    {
        await ticketEventService.DeleteTicketEventAsync(ticketEventId);

        return NoContent();
    }

    [HttpPut("approval", Name = "TicketEventApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> TicketEventApprovalAsync([FromQuery] TicketEventApprovalRequest approvalRequest)
    {
        await ticketEventService.TicketEventApprovalAsync(approvalRequest);

        return NoContent();
    }
}

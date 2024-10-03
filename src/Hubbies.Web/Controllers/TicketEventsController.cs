using Hubbies.Application.Features.TicketEvents;
using Hubbies.Domain.Enums;

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

    [HttpGet("{id}", Name = "GetTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketEventAsync(
        Guid id,
        [FromQuery] TicketEventIncludeParameter includeParameter)
    {
        var response = await ticketEventService.GetTicketEventAsync(id, includeParameter);

        return Ok(response);
    }

    [HttpPost(Name = "CreateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateTicketEventAsync(CreateTicketEventRequest request)
    {
        var response = await ticketEventService.CreateTicketEventAsync(request);

        return CreatedAtRoute("GetTicketEvent", new { id = response.Id }, response);
    }

    [HttpPut("{id}", Name = "UpdateTicketEvent")]
    [ProducesResponseType(typeof(TicketEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateTicketEventAsync(Guid id, UpdateTicketEventRequest request)
    {
        var response = await ticketEventService.UpdateTicketEventAsync(id, request);

        return Ok(response);
    }

    [HttpDelete("{id}", Name = "DeleteTicketEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicketEventAsync(Guid id)
    {
        await ticketEventService.DeleteTicketEventAsync(id);

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

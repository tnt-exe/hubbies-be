using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class FeedbacksController(IFeedbackService feedbackService)
    : ControllerBase
{
    [HttpGet(Name = "GetFeedbacks")]
    [ProducesResponseType(typeof(PaginationResponse<FeedbackDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbacksAsync([FromQuery] FeedbackQueryParameter queryParameter)
    {
        var response = await feedbackService.GetFeedbacksAsync(queryParameter);

        return Ok(response);
    }

    [HttpGet("user/{userId:guid}/ticket-event/{ticketEventId:guid}", Name = "GetFeedback")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        var response = await feedbackService.GetFeedbackAsync(userId, ticketEventId);

        return Ok(response);
    }

    [Authorize(Policy.Customer)]
    [HttpPost(Name = "CreateFeedback")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateFeedbackAsync(CreateFeedbackRequest request)
    {
        var response = await feedbackService.CreateFeedbackAsync(request);

        return CreatedAtRoute("GetFeedback", new { userId = response.UserId, ticketEventId = response.TicketEventId }, response);
    }

    [Authorize(Policy.Customer)]
    [HttpPut("ticket-event/{ticketEventId:guid}", Name = "UpdateFeedback")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateFeedbackAsync(Guid ticketEventId, UpdateFeedbackRequest request)
    {
        var response = await feedbackService.UpdateFeedbackAsync(ticketEventId, request);

        return Ok(response);
    }

    [Authorize(Policy.Customer)]
    [HttpDelete("user/{userId:guid}/ticket-event/{ticketEventId:guid}", Name = "DeleteFeedback")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        await feedbackService.DeleteFeedbackAsync(userId, ticketEventId);

        return NoContent();
    }

    [Authorize(Policy.Admin)]
    [HttpPut("approval", Name = "FeedbackApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FeedbackApprovalAsync([FromQuery] FeedbackApprovalRequest approvalRequest)
    {
        await feedbackService.FeedbackApprovalAsync(approvalRequest);

        return NoContent();
    }
}

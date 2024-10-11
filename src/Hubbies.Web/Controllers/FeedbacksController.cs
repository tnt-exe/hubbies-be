using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class FeedbacksController(IFeedbackService feedbackService)
    : ControllerBase
{
    /// <summary>
    /// Get feedbacks with pagination
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Return feedbacks with pagination</response>
    [HttpGet(Name = "GetFeedbacks")]
    [ProducesResponseType(typeof(PaginationResponse<FeedbackDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeedbacksAsync([FromQuery] FeedbackQueryParameter queryParameter)
    {
        var response = await feedbackService.GetFeedbacksAsync(queryParameter);

        return Ok(response);
    }

    /// <summary>
    /// Get feedback by user id and ticket event id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ticketEventId"></param>
    /// <response code="200">Return feedback</response>
    /// <response code="404">Feedback not found</response>
    [HttpGet("user/{userId:guid}/ticket-event/{ticketEventId:guid}", Name = "GetFeedback")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        var response = await feedbackService.GetFeedbackAsync(userId, ticketEventId);

        return Ok(response);
    }

    /// <summary>
    /// Create feedback
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/feedbacks
    ///     {
    ///         "ticketEventId": "00000000-0000-0000-0000-000000000000",
    ///         "content": "This event succ",
    ///         "rating": 2
    ///     }
    ///    
    /// </remarks>
    /// <response code="201">Return created feedback</response>
    /// <response code="422">Validation error</response>
    [Authorize(Policy.Customer)]
    [HttpPost(Name = "CreateFeedback")]
    [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateFeedbackAsync(CreateFeedbackRequest request)
    {
        var response = await feedbackService.CreateFeedbackAsync(request);

        return CreatedAtRoute("GetFeedback", new { userId = response.UserId, ticketEventId = response.TicketEventId }, response);
    }

    /// <summary>
    /// Update feedback
    /// </summary>
    /// <param name="ticketEventId"></param>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/feedbacks/ticket-event/{ticketEventId}
    ///     {
    ///         "content": "This event is not that succ",
    ///         "rating": 4
    ///     }
    ///    
    /// </remarks>
    /// <response code="200">Return updated feedback</response>
    /// <response code="404">Feedback not found</response>
    /// <response code="422">Validation error</response>
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

    /// <summary>
    /// Delete feedback
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ticketEventId"></param>
    /// <response code="204">Feedback deleted</response>
    /// <response code="404">Feedback not found</response>
    [Authorize(Policy.Customer)]
    [HttpDelete("user/{userId:guid}/ticket-event/{ticketEventId:guid}", Name = "DeleteFeedback")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFeedbackAsync(Guid userId, Guid ticketEventId)
    {
        await feedbackService.DeleteFeedbackAsync(userId, ticketEventId);

        return NoContent();
    }

    /// <summary>
    /// Feedback approval
    /// </summary>
    /// <param name="approvalRequest"></param>
    /// <response code="204">Feedback approved</response>
    /// <response code="404">Feedback either is deleted, or not having 'Pending' status</response>
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

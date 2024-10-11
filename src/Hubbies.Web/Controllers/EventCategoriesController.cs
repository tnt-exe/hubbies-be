using Hubbies.Application.Features.EventCategories;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class EventCategoriesController(IEventCategoryService eventCategoryService)
    : ControllerBase
{
    /// <summary>
    /// Get all event categories
    /// </summary>
    /// <response code="200">Returns all event categories</response>
    [HttpGet(Name = "GetEventCategories")]
    [ProducesResponseType(typeof(IEnumerable<EventCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventCategoriesAsync()
    {
        var response = await eventCategoryService.GetEventCategoriesAsync();

        return Ok(response);
    }

    /// <summary>
    /// Get event category by id
    /// </summary>
    /// <param name="eventCategoryId"></param>
    /// <response code="200">Returns event category</response>
    /// <response code="404">Event category not found</response>
    [HttpGet("{eventCategoryId:guid}", Name = "GetEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEventCategoryAsync(Guid eventCategoryId)
    {
        var response = await eventCategoryService.GetEventCategoryAsync(eventCategoryId);

        return Ok(response);
    }

    /// <summary>
    /// Create event category
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/event-categories
    ///     {
    ///         "address": "HCM",
    ///         "name": "Cofi"
    ///     }
    ///    
    /// </remarks>
    /// <response code="201">Event category created</response>
    /// <response code="422">Unprocessable entity</response>
    [Authorize(Policy.Admin)]
    [HttpPost(Name = "CreateEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateEventCategoryAsync(CreateEventCategoryRequest request)
    {
        var response = await eventCategoryService.CreateEventCategoryAsync(request);

        return CreatedAtRoute("GetEventCategory", new { eventCategoryId = response.Id }, response);
    }

    /// <summary>
    /// Update event category
    /// </summary>
    /// <param name="eventCategoryId"></param>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     
    ///     PUT /api/event-categories/{eventCategoryId}
    ///     {
    ///         "address": "HCM",
    ///         "name": "Cofi"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Event category updated</response>
    /// <response code="404">Event category not found</response>
    /// <response code="422">Unprocessable entity</response>
    [Authorize(Policy.Admin)]
    [HttpPut("{eventCategoryId:guid}", Name = "UpdateEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateEventCategoryAsync(Guid eventCategoryId, UpdateEventCategoryRequest request)
    {
        var response = await eventCategoryService.UpdateEventCategoryAsync(eventCategoryId, request);

        return Ok(response);
    }

    /// <summary>
    /// Delete event category
    /// </summary>
    /// <param name="eventCategoryId"></param>
    /// <response code="204">Event category deleted</response>
    /// <response code="404">Event category not found</response>
    [Authorize(Policy.Admin)]
    [HttpDelete("{eventCategoryId:guid}", Name = "DeleteEventCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEventCategoryAsync(Guid eventCategoryId)
    {
        await eventCategoryService.DeleteEventCategoryAsync(eventCategoryId);

        return NoContent();
    }
}

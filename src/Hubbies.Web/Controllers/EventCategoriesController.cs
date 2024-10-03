using Hubbies.Application.Features.EventCategories;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class EventCategoriesController(IEventCategoryService eventCategoryService)
    : ControllerBase
{
    [HttpGet(Name = "GetEventCategories")]
    [ProducesResponseType(typeof(IEnumerable<EventCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventCategoriesAsync()
    {
        var response = await eventCategoryService.GetEventCategoriesAsync();

        return Ok(response);
    }

    [HttpGet("{eventCategoryId}", Name = "GetEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEventCategoryAsync(Guid eventCategoryId)
    {
        var response = await eventCategoryService.GetEventCategoryAsync(eventCategoryId);

        return Ok(response);
    }

    [HttpPost(Name = "CreateEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateEventCategoryAsync(CreateEventCategoryRequest request)
    {
        var response = await eventCategoryService.CreateEventCategoryAsync(request);

        return CreatedAtRoute("GetEventCategory", new { eventCategoryId = response.Id }, response);
    }

    [HttpPut("{eventCategoryId}", Name = "UpdateEventCategory")]
    [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateEventCategoryAsync(Guid eventCategoryId, UpdateEventCategoryRequest request)
    {
        var response = await eventCategoryService.UpdateEventCategoryAsync(eventCategoryId, request);

        return Ok(response);
    }

    [HttpDelete("{eventCategoryId}", Name = "DeleteEventCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEventCategoryAsync(Guid eventCategoryId)
    {
        await eventCategoryService.DeleteEventCategoryAsync(eventCategoryId);

        return NoContent();
    }
}

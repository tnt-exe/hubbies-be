using Hubbies.Application.Features.EventCategories;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IEventCategoryService
{
    /// <summary>
    /// Get all event categories
    /// </summary>
    /// <returns>
    /// The task result contains a collection of <see cref="EventCategoryDto"/> objects.
    /// </returns>
    Task<IEnumerable<EventCategoryDto>> GetEventCategoriesAsync();

    /// <summary>
    /// Get event category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// The task result contains an <see cref="EventCategoryDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Event category not found</exception>
    Task<EventCategoryDto> GetEventCategoryAsync(Guid id);

    /// <summary>
    /// Create event category
    /// </summary>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains an <see cref="EventCategoryDto"/> object.
    /// </returns>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<EventCategoryDto> CreateEventCategoryAsync(CreateEventCategoryRequest request);

    /// <summary>
    /// Update event category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns>
    /// The task result contains an <see cref="EventCategoryDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Event category not found</exception>
    /// <exception cref="ValidationException">Validation error</exception>
    Task<EventCategoryDto> UpdateEventCategoryAsync(Guid id, UpdateEventCategoryRequest request);

    /// <summary>
    /// Delete event category
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException">Event category not found</exception>
    Task DeleteEventCategoryAsync(Guid id);
}

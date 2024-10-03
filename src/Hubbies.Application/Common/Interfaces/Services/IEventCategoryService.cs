using Hubbies.Application.Features.EventCategories;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IEventCategoryService
{
    Task<IEnumerable<EventCategoryDto>> GetEventCategoriesAsync();

    Task<EventCategoryDto> GetEventCategoryAsync(Guid id);

    Task<EventCategoryDto> CreateEventCategoryAsync(CreateEventCategoryRequest request);

    Task<EventCategoryDto> UpdateEventCategoryAsync(Guid id, UpdateEventCategoryRequest request);

    Task DeleteEventCategoryAsync(Guid id);
}

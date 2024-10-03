using Hubbies.Application.Features.EventCategories;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void EventCategoryProfile()
    {
        CreateMap<EventCategory, EventCategoryDto>().ReverseMap();
        CreateMap<EventCategory, CreateEventCategoryRequest>().ReverseMap();
        CreateMap<EventCategory, UpdateEventCategoryRequest>().ReverseMap();
    }
}

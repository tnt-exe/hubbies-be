using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void TicketEventProfile()
    {
        CreateMap<TicketEvent, TicketEventDto>().ReverseMap();
        CreateMap<TicketEvent, CreateTicketEventRequest>().ReverseMap();
        CreateMap<TicketEvent, UpdateTicketEventRequest>().ReverseMap();
    }
}
using Hubbies.Application.Features.TicketEvents;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void TicketEventProfile()
    {
        CreateMap<TicketEvent, TicketEventDto>()
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToLocalTime()))
            .ReverseMap();

        CreateMap<TicketEvent, CreateTicketEventRequest>()
            .ReverseMap()
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToUniversalTime()));

        CreateMap<TicketEvent, UpdateTicketEventRequest>()
            .ReverseMap()
            .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => src.PostDate.ToUniversalTime()));
    }
}
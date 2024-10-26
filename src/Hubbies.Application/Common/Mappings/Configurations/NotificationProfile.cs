using Hubbies.Application.Features.Notifications;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>()
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt!.Value.ToLocalTime()))
            .ReverseMap();
    }
}

using Hubbies.Application.Features.Notifications;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>().ReverseMap();
    }
}

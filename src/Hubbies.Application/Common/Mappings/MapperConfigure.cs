namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    public MapperConfigure()
    {
        AccountProfile();
        EventCategoryProfile();
        TicketEventProfile();
        FeedbackProfile();
        OrderProfile();
    }
}

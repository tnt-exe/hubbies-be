using Hubbies.Application.Features.Feedbacks;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void FeedbackProfile()
    {
        CreateMap<Feedback, FeedbackDto>().ReverseMap();
        CreateMap<Feedback, CreateFeedbackRequest>().ReverseMap();
        CreateMap<Feedback, UpdateFeedbackRequest>().ReverseMap();
    }
}

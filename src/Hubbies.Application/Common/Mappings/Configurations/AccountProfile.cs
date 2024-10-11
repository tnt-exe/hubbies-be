using Hubbies.Application.Features.Accounts;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void AccountProfile()
    {
        CreateMap<ApplicationUser, AccountDto>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.GetValueOrDefault().ToLocalTime()))
            .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.GetValueOrDefault().ToLocalTime()))
            .ReverseMap();

        CreateMap<ApplicationUser, UpdateAccountInformationRequest>()
            .ReverseMap()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.GetValueOrDefault().ToUniversalTime()));
    }
}

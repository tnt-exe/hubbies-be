using Hubbies.Application.Features.Accounts;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void AccountProfile()
    {
        CreateMap<ApplicationUser, AccountDto>().ReverseMap();
    }
}

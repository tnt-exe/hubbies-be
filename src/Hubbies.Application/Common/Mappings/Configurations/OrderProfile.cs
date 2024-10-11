using Hubbies.Application.Features.OrderDetails;
using Hubbies.Application.Features.Orders;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, CreateOrderRequest>().ReverseMap();

        CreateMap<OrderDetails, OrderDetailsDto>()
            .ForMember(dest => dest.PreferredTime, opt => opt.MapFrom(src => src.PreferredTime.ToLocalTime()))
            .ReverseMap();

        CreateMap<OrderDetails, CreateOrderDetailsRequest>()
            .ReverseMap()
            .ForMember(dest => dest.PreferredTime, opt => opt.MapFrom(src => src.PreferredTime.ToUniversalTime()));
    }
}

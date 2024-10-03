using Hubbies.Application.Features.OrderDetails;
using Hubbies.Application.Features.Orders;

namespace Hubbies.Application.Common.Mappings;

public partial class MapperConfigure : Profile
{
    void OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, CreateOrderRequest>().ReverseMap();

        CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
        CreateMap<OrderDetails, CreateOrderDetailsRequest>().ReverseMap();
    }
}
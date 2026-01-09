using AutoMapper;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Presenter.Mappers;

public class OrderItemMapper : Profile
{

    public OrderItemMapper()
    {
        CreateMap<OrderItem, OrderItemDto>()
            .ReverseMap();
    }

}

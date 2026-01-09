using AutoMapper;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Data.Model;

namespace Restaurant.Preparation.Presenter.Mappers;

public class OrderMapper : Profile
{

    public OrderMapper()
    {
        CreateMap<OrderDto, OrderData>()
            .ForPath(d => d.Cliente, o => o.MapFrom(s => s.Cliente!.Id))
            .ReverseMap()
            .ForPath(d => d.Cliente!.Id, o => o.MapFrom(s => s.Cliente));
    }

}

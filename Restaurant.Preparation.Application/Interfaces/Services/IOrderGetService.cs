using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.Services;

public interface IOrderGetService
{

    Task<OrderDto> Get(string orderId);

}

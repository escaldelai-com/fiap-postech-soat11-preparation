using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.Facade;

public interface IOrderFacade
{

    Task<OrderDto> Confirm(OrderDto order);

    Task<OrderDto> Prepare(string orderId);
    
    Task<OrderDto> Delivery(string orderId);
    
    Task<OrderDto> Finalize(string orderId);

}

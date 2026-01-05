using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.UseCases;

public interface IOrderDeliveryUseCase
{

    Task<OrderDto> Delivery(string orderId);

}

using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.UseCases;

public interface IOrderConfirmUseCase
{

    Task<OrderDto> Confirm(OrderDto order);

}

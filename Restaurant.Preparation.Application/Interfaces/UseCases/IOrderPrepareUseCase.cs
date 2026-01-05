using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.UseCases;

public interface IOrderPrepareUseCase
{

    Task<OrderDto> Prepare(string orderId);

}

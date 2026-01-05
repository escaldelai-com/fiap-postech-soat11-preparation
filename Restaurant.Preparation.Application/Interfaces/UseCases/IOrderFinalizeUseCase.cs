using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.UseCases;

public interface IOrderFinalizeUseCase
{

    Task<OrderDto> Finalize(string orderId);

}

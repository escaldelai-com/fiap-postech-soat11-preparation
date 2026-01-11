using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.Services;

public class OrderGetService(
    IOrderRepository repo) : IOrderGetService
{

    public async Task<OrderDto> Get(string? orderId)
    {
        var order = await repo.Get(orderId);

        if (order == null)
            throw new NotFoundException(orderId!);

        return order;
    }

}

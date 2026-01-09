using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.Interfaces.UseCases;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.UseCases;

public class OrderPrepareUseCase(
    IOrderRepository repo,
    IOrderGetService orderGet,
    IOrderCreateService orderCreate) : IOrderPrepareUseCase
{

    public async Task<OrderDto> Prepare(string orderId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .Validate();

        var order = await orderGet.Get(orderId);
        var model = orderCreate.Create(order);

        model.Prepare();
        order.Status = model.Status;

        return await repo.Update(order);
    }

}

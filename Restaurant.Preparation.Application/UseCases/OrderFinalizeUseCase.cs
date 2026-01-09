using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.Interfaces.UseCases;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.UseCases;

public class OrderFinalizeUseCase(
    IOrderRepository repo,
    IOrderGetService orderGet,
    IOrderCreateService orderCreate) : IOrderFinalizeUseCase
{

    public async Task<OrderDto> Finalize(string orderId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .Validate();

        var order = await orderGet.Get(orderId);
        var model = orderCreate.Create(order);

        model.OrderFinalize();
        order.Status = model.Status;

        return await repo.Update(order);
    }


}

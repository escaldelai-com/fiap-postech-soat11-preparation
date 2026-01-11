using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.Interfaces.UseCases;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.UseCases;

public class OrderDeliveryUseCase(
    IOrderRepository repo,
    IOrderGetService orderGet,
    IOrderCreateService orderCreate) : IOrderDeliveryUseCase
{

    public async Task<OrderDto> Delivery(string orderId)
    {
        Validator.Create()
            .IsNotNullOrWhiteSpace(orderId)
            .Validate();

        var order = await orderGet.Get(orderId);
        var model = orderCreate.Create(order);

        model.Delivery();
        order.Status = model.Status;

        return await repo.Update(order);
    }

}

using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Application.Interfaces.UseCases;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.UseCases;

public class OrderConfirmUseCase(
    IOrderRepository repo,
    IOrderCreateService orderCreate) : IOrderConfirmUseCase
{

    public async Task<OrderDto> Confirm(OrderDto order)
    {
        Validator.Create()
            .IsNotNull(order)
            .Validate();

        var model = orderCreate.Create(order);

        model.Confirm();
        order.Status = model.Status;

        return await repo.Create(order);
    }

}

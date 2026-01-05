using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Facade;
using Restaurant.Preparation.Application.Interfaces.UseCases;

namespace Restaurant.Preparation.Facade;

public class OrderFacade(
    IOrderConfirmUseCase orderConfirm,
    IOrderPrepareUseCase orderPrepare,
    IOrderDeliveryUseCase orderDelivery,
    IOrderFinalizeUseCase orderFinalize) : IOrderFacade
{

    public async Task<OrderDto> Confirm(OrderDto order)
    {
        return await orderConfirm.Confirm(order);
    }

    public async Task<OrderDto> Prepare(string orderId)
    {
        return await orderPrepare.Prepare(orderId);
    }

    public async Task<OrderDto> Delivery(string orderId)
    {
        return await orderDelivery.Delivery(orderId);
    }

    public async Task<OrderDto> Finalize(string orderId)
    {
        return await orderFinalize.Finalize(orderId);
    }

}

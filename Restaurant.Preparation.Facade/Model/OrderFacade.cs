using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.ExternalServices;
using Restaurant.Preparation.Application.Interfaces.Facade;
using Restaurant.Preparation.Application.Interfaces.Repository;
using Restaurant.Preparation.Application.Interfaces.UseCases;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Facade;

public class OrderFacade(
    IOrderRepository repo,
    IIdentificationService idService,
    IOrderConfirmUseCase orderConfirm,
    IOrderPrepareUseCase orderPrepare,
    IOrderDeliveryUseCase orderDelivery,
    IOrderFinalizeUseCase orderFinalize) : IOrderFacade
{

    public async Task<IEnumerable<OrderDto>> GetWaiting()
    {
        var orders = await repo.GetListByStatuses(
            OrderStatus.Received,
            OrderStatus.Preparing,
            OrderStatus.Delivery);

        if (!orders.Any())
            return orders;

        var dic = await GetClients(orders);

        foreach (var order in orders)
        {
            if (dic.TryGetValue(order.Cliente!.Id!, out var client))
                order.Cliente = client;
        }

        return orders;
    }

    public async Task<OrderDto> Confirm(OrderDto order)
    {
        var data = await orderConfirm.Confirm(order);

        data.Cliente = await idService.GetById(data.Cliente!.Id!);

        return data;
    }

    public async Task<OrderDto> Prepare(string orderId)
    {
        var data = await orderPrepare.Prepare(orderId);

        data.Cliente = await idService.GetById(data.Cliente!.Id!);

        return data;
    }

    public async Task<OrderDto> Delivery(string orderId)
    {
        var data = await orderDelivery.Delivery(orderId);

        data.Cliente = await idService.GetById(data.Cliente!.Id!);

        return data;
    }

    public async Task<OrderDto> Finalize(string orderId)
    {
        var data = await orderFinalize.Finalize(orderId);

        data.Cliente = await idService.GetById(data.Cliente!.Id!);

        return data;
    }



    private async Task<Dictionary<string, ClientDto>> GetClients(IEnumerable<OrderDto> orders)
    {
        var ids = orders.Select(o => o.Cliente!.Id).Cast<string>();
        var data = await idService.Get(ids);

        return data.ToDictionary(c => c.Id!);
    }

}

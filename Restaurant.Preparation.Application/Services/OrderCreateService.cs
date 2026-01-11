using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Services;
using Restaurant.Preparation.Domain;

namespace Restaurant.Preparation.Application.Services;

public class OrderCreateService : IOrderCreateService
{

    public Order Create(OrderDto order)
    {
        return new Order(
            order.Data!.Value,
            order.Numero!.Value,
            order.Cliente!.Id!,
            order.Status!,
            order.Items
                .Select(x => new OrderItem(x.Nome!, x.Tipo!))
                .ToArray());
    }

}

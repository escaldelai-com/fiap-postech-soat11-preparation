using Bogus;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.Test;

public abstract class TestBase
{

    protected Faker faker = new("pt_BR");

    protected string GetGuid() => Guid.NewGuid().ToString("n");



    protected OrderDto GetOrderDto(int items = 3, string status = OrderStatus.Paid)
    {
        return new OrderDto
        {
            Id = GetGuid(),
            Data = faker.Date.Past(),
            Numero = faker.Random.Int(1, 1000),
            Cliente = new ClientDto
            {
                Id = GetGuid(),
                Nome = faker.Name.FullName()
            },
            Status = status,
            Items = faker.Make(items, GetItemDto).ToArray()            
        };
    }

    protected Order GetOrder(int items = 3, string status = OrderStatus.Paid)
    {
        return new Order(
            faker.Date.Past(),
            faker.Random.Int(1, 1000),
            GetGuid(),
            status,
            faker.Make(items, GetItem).ToArray()
        );
    }

    protected Order GetOrder(OrderDto order)
    {
        return new Order(
            order.Data!.Value,
            order.Numero!.Value,
            order.Cliente!.Id!,
            order.Status!,
            order.Items!.Select(GetItem).ToArray()
        );
    }

    protected OrderItemDto GetItemDto()
    {
        return new OrderItemDto
        {
            Id = GetGuid(),
            Nome = faker.Commerce.ProductName(),
            Tipo = faker.Commerce.Categories(1).First()
        };
    }

    protected OrderItem GetItem()
    {
        return new OrderItem(
            faker.Commerce.ProductName(),
            faker.Commerce.Categories(1).First()
        );
    }

    protected OrderItem GetItem(OrderItemDto item)
    {
        return new OrderItem(item.Nome!, item.Tipo!);
    }

}

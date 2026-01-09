using Restaurant.Preparation.Application.DTO;

namespace Restaurant.Preparation.Application.Interfaces.Repository;

public interface IOrderRepository
{

    Task<OrderDto?> Get(string? orderId);

    Task<IEnumerable<OrderDto>> GetListByStatuses(params string[] statuses);

    Task<OrderDto> Create(OrderDto order);

    Task<OrderDto> Update(OrderDto order);

}

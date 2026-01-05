using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Model;

namespace Restaurant.Preparation.Application.Interfaces.Services;

public interface IOrderCreateService
{

    Order Create(OrderDto order);

}

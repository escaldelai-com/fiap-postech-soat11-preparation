using Microsoft.AspNetCore.Mvc;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Facade;

namespace Restaurant.Preparation.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm([FromBody] OrderDto order)
    {
        var result = await facade.Confirm(order);

        return Ok(result);
    }

    [HttpPost("prepare")]
    public async Task<IActionResult> Prepare([FromBody] OrderRequestDto order)
    {
        var result = await facade.Prepare(order.OrderId!);

        return Ok(result);
    }

    [HttpPost("delivery")]
    public async Task<IActionResult> Delivery([FromBody] OrderRequestDto order)
    {
        var result = await facade.Delivery(order.OrderId!);

        return Ok(result);
    }

    [HttpPost("finalize")]
    public async Task<IActionResult> Finalize([FromBody] OrderRequestDto order)
    {
        var result = await facade.Finalize(order.OrderId!);

        return Ok(result);
    }

}

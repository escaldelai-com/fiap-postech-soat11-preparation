using Microsoft.AspNetCore.Mvc;
using Restaurant.Preparation.WebApi.Security;
using Restaurant.Preparation.Application.DTO;
using Restaurant.Preparation.Application.Interfaces.Facade;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Preparation.WebApi.Controllers;

[Route("[controller]")]
public class OrderController(
    IOrderFacade facade) : Controller
{

    [HttpGet("waiting")]
    [Authorize(Claims.Order.GetWaiting)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
    public async Task<IActionResult> GetWaiting()
    {
        var result = await facade.GetWaiting();

        return Ok(result);
    }

    [HttpPost("confirm")]
    [Authorize(Claims.Order.Confirm)]
    public async Task<IActionResult> Confirm([FromBody] OrderDto order)
    {
        var result = await facade.Confirm(order);

        return Ok(result);
    }

    [HttpPost("prepare")]
    [Authorize(Claims.Order.Prepare)]
    public async Task<IActionResult> Prepare([FromBody] OrderRequestDto order)
    {
        var result = await facade.Prepare(order.OrderId!);

        return Ok(result);
    }

    [HttpPost("delivery")]
    [Authorize(Claims.Order.Delivery)]
    public async Task<IActionResult> Delivery([FromBody] OrderRequestDto order)
    {
        var result = await facade.Delivery(order.OrderId!);

        return Ok(result);
    }

    [HttpPost("finalize")]
    [Authorize(Claims.Order.Finalize)]
    public async Task<IActionResult> Finalize([FromBody] OrderRequestDto order)
    {
        var result = await facade.Finalize(order.OrderId!);

        return Ok(result);
    }

}

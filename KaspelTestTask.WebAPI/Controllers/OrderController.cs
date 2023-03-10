using KaspelTestTask.Application.Features.Orders.Commands;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _mediator;

    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpGet("")]
    public async Task<IEnumerable<OrderDto>> GetOrders(Guid? id, DateOnly? orderDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredOrdersQuery(id, orderDate), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(IEnumerable<OrderedBookRequestDto> orderedBooks, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new CreateOrderCommand(orderedBooks), cancellationToken);
        return Created($"/api/Order/{order!.Id}", order);
    }
}

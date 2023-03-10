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

    [HttpGet("")]
    public async Task<IEnumerable<OrderBrief>> GetOrders(Guid? id, DateOnly? orderDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredOrdersQuery(id, orderDate), cancellationToken);
    }

    [HttpPost]
    public async Task<OrderBrief?> CreateOrder(IEnumerable<OrderedBookRequestBrief> orderedBooks, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateOrderCommand(orderedBooks), cancellationToken);
    }
}

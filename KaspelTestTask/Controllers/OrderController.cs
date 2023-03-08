using KaspelTestTask.Application.Features.BookService.Queries;
using KaspelTestTask.Application.Features.OrderService.Commands;
using KaspelTestTask.Application.Features.OrderService.Models;
using KaspelTestTask.Application.Features.OrderService.Queries;
using KaspelTestTask.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _mediator;

    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetAllOrders(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetFilteredOrders(Guid? id, DateTime? orderDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredOrdersQuery(id, orderDate), cancellationToken);
    }

    [HttpPost]
    public async Task<Order?> CreateOrder(IEnumerable<OrderedBookBrief> orderedBooks, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateOrderCommand(orderedBooks), cancellationToken);
    }
}

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
    public async Task<IEnumerable<OrderBrief>> GetFilteredOrders(Guid? id, DateOnly? orderDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredOrdersQuery(id, orderDate), cancellationToken);
    }

    [HttpPost]
    public async Task<OrderBrief?> CreateOrder(IEnumerable<OrderedBookRequestBrief> orderedBooks, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateOrderCommand(orderedBooks), cancellationToken);
    }
}

using KaspelTestTask.Application.Features.BookService.Queries;
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

}

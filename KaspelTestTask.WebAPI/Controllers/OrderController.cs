using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Application.Features.Orders.Commands;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

/// <summary>
/// Order controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor of Order controller. Here MediatR gets injected.
    /// </summary>
    /// <param name="mediator"></param>
    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get information on the order by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Order is returned</response>
    /// <response code="404">Order with specified ID is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    /// <summary>
    /// Get information on all orders available. Output can be filtered by ID and/or Order date.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orderDate"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Array of Orders is returned</response>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
    public async Task<IEnumerable<OrderDto>> GetOrders(Guid? id, DateOnly? orderDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredOrdersQuery(id, orderDate), cancellationToken);
    }

    /// <summary>
    /// Create Order. Body should contain array of Book IDs and quantity for each book. 
    /// </summary>
    /// <param name="orderedBooks"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="201">Order is created. Object with order info is returned</response>
    /// <response code="400">Bad request. The cause might be malformed request syntax</response>
    /// <response code="422">Order cannot be created. This happens when book IDs are incorrect, or quantity is less or equal to 0.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<OrderDto>> CreateOrder(IEnumerable<OrderedBookRequestDto> orderedBooks, CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new CreateOrderCommand(orderedBooks), cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new { id = order!.Id}, order);
    }
}

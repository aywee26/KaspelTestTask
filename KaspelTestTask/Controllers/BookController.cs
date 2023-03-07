using KaspelTestTask.Application.Features.BookService.Queries;
using KaspelTestTask.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ISender _mediator;

    public BookController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Book?> GetBook(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
    }
}

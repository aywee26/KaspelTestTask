using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Application.Features.Books.Queries;
using MediatR;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id, CancellationToken cancellationToken)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpGet("")]
    public async Task<IEnumerable<BookDto>> GetBooks(string? title, DateOnly? publicationDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredBooksQuery(title, publicationDate), cancellationToken);
    }
}

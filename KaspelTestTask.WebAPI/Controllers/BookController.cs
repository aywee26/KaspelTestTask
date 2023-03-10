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
    public async Task<BookDto?> GetBook(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
    }

    [HttpGet("")]
    public async Task<IEnumerable<BookDto>> GetBooks(string? title, DateOnly? publicationDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredBooksQuery(title, publicationDate), cancellationToken);
    }
}

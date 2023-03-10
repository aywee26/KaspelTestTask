using KaspelTestTask.Application.Features.BookService.Models;
using KaspelTestTask.Application.Features.BookService.Queries;
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
    public async Task<BookBrief?> GetBook(Guid id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
    }

    [HttpGet("")]
    public async Task<IEnumerable<BookBrief>> GetBooks(string? title, DateOnly? publicationDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredBooksQuery(title, publicationDate), cancellationToken);
    }
}

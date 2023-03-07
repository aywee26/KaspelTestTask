using KaspelTestTask.Application.Features.BookService.Queries;
using KaspelTestTask.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
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

    [HttpGet]
    public async Task<IEnumerable<Book>> GetFilteredBooks(string? title, DateOnly? publicationDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredBooksQuery(title, publicationDate), cancellationToken);
    }
}

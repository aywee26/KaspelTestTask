using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KaspelTestTask.WebAPI.Controllers;

/// <summary>
/// Book controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Constructor of Book controller. Here MediatR gets injected.
    /// </summary>
    /// <param name="mediator"></param>
    public BookController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get information on the book by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <response code="200">Book object is returned</response>
    /// <response code="404">Book with specified ID is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> GetBook(Guid id, CancellationToken cancellationToken)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
        return book is null ? NotFound() : Ok(book);
    }

    /// <summary>
    /// Get information on all books available. Output can be filtered by Title and/or Publication date.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="publicationDate"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Array of book objects is returned</response>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookDto>))]
    public async Task<IEnumerable<BookDto>> GetBooks(string? title, DateOnly? publicationDate, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFilteredBooksQuery(title, publicationDate), cancellationToken);
    }
}

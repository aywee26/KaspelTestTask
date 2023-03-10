namespace KaspelTestTask.Application.Features.Books.Models;

public record BookDto(Guid Id, string Title, string Author, DateOnly PublicationDate, decimal Price);
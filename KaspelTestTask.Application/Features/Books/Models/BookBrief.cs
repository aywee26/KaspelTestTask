namespace KaspelTestTask.Application.Features.Books.Models;

public record BookBrief(Guid Id, string Title, string Author, DateOnly PublicationDate, decimal Price);
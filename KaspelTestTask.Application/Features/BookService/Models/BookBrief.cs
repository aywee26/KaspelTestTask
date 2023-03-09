namespace KaspelTestTask.Application.Features.BookService.Models;

public record BookBrief(Guid Id, string Title, string Author, DateOnly PublicationDate, decimal Price);
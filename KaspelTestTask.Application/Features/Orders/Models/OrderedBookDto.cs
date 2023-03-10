namespace KaspelTestTask.Application.Features.Orders.Models;

public record OrderedBookDto(Guid Id, string Title, int Quantity, decimal Price);
namespace KaspelTestTask.Application.Features.Orders.Models;

public record OrderedBookBrief(Guid Id, string Title, int Quantity, decimal Price);
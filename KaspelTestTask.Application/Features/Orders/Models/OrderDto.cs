namespace KaspelTestTask.Application.Features.Orders.Models;

public record OrderDto(Guid Id, DateOnly OrderDate, IEnumerable<OrderedBookDto> OrderedBooks);

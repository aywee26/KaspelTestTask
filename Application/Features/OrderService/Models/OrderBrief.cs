using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Features.OrderService.Models;

public record OrderBrief(Guid Id, DateOnly OrderDate, IEnumerable<OrderedBookBrief> OrderedBooks);

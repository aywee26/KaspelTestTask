using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Repositories.Abstractions;

public interface IOrdersRepository
{
    Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetFilteredOrdersAsync(Guid? id = null, DateOnly? orderDate = null, CancellationToken cancellationToken = default);

    Task<Order?> CreateOrderAsync(Order order, IEnumerable<OrderedBook> orderedBooks, CancellationToken cancellationToken = default);
}

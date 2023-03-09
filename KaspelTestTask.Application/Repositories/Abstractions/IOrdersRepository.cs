using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Repositories.Abstractions;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetFilteredOrdersAsync(Guid? id = null, DateOnly? orderDate = null, CancellationToken cancellationToken = default);

    Task<Order?> CreateOrder(Order order, IEnumerable<OrderedBook> orderedBooks, CancellationToken cancellationToken = default);
}

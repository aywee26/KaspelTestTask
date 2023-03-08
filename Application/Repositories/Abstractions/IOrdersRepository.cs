using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Repositories.Abstractions;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
}

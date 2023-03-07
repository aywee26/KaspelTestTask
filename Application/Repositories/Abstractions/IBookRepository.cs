using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Repositories.Abstractions;

public interface IBookRepository
{
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

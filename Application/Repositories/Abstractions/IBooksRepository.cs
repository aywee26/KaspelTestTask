using KaspelTestTask.Domain.Entities;

namespace KaspelTestTask.Application.Repositories.Abstractions;

public interface IBooksRepository
{
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Book>> GetFilteredBooksAsync(string? title = null, DateOnly? publicationDate = null, CancellationToken cancellationToken = default);
}

using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _dbContext;

    public BooksRepository(AppDbContext dbContext)
    {
        _dbContext = Guard.Against.Null(dbContext, nameof(dbContext));
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetFilteredBooksAsync(string? title = null, DateOnly? publicationDate = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Book> query = _dbContext.Books;

        if (title is not null)
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (publicationDate is not null)
        {
            query = query.Where(b => b.PublicationDate.Equals(publicationDate));
        }

        return await query.ToListAsync(cancellationToken);
    }
}

using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.BookService.Queries;

public record GetFilteredBooksQuery(string? Title, DateOnly? PublicationDate) : IRequest<IEnumerable<Book>>
{
    public class Handler : IRequestHandler<GetFilteredBooksQuery, IEnumerable<Book>>
    {
        private readonly IBooksRepository _bookRepository;

        public Handler(IBooksRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            return await _bookRepository.GetFilteredBooksAsync(request.Title, request.PublicationDate, cancellationToken);
        }
    }

}

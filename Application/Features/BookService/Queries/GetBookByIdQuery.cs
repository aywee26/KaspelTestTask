using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.BookService.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<Book?>
{
    public class Handler : IRequestHandler<GetBookByIdQuery, Book?>
    {
        private readonly IBookRepository _bookRepository;

        public Handler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            return await _bookRepository.GetBookByIdAsync(request.Id, cancellationToken);
        }
    }
}

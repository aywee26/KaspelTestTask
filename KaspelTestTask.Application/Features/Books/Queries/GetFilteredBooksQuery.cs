using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using MediatR;

namespace KaspelTestTask.Application.Features.Books.Queries;

public record GetFilteredBooksQuery(string? Title, DateOnly? PublicationDate) : IRequest<IEnumerable<BookBrief>>
{
    public class Handler : IRequestHandler<GetFilteredBooksQuery, IEnumerable<BookBrief>>
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IMapper _mapper;

        public Handler(IBooksRepository bookRepository, IMapper mapper)
        {
            _bookRepository = Guard.Against.Null(bookRepository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<IEnumerable<BookBrief>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            var books = await _bookRepository.GetFilteredBooksAsync(request.Title, request.PublicationDate, cancellationToken);
            var result = _mapper.Map<IEnumerable<BookBrief>>(books);
            return result;
        }
    }

}

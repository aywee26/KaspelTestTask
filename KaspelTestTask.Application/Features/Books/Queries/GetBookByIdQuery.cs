using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.Books.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using MediatR;

namespace KaspelTestTask.Application.Features.Books.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<BookBrief?>
{
    public class Handler : IRequestHandler<GetBookByIdQuery, BookBrief?>
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IMapper _mapper;

        public Handler(IBooksRepository bookRepository, IMapper mapper)
        {
            _bookRepository = Guard.Against.Null(bookRepository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<BookBrief?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            var book = await _bookRepository.GetBookByIdAsync(request.Id, cancellationToken);
            var result = _mapper.Map<BookBrief>(book);
            return result;
        }
    }
}

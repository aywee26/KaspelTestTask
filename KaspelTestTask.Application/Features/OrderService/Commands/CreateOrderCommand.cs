using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.OrderService.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using KaspelTestTask.Domain.Exceptions;
using MediatR;

namespace KaspelTestTask.Application.Features.OrderService.Commands;

public record CreateOrderCommand(IEnumerable<OrderedBookRequestBrief> OrderedBooks) : IRequest<OrderBrief?>
{
    public class Handler : IRequestHandler<CreateOrderCommand, OrderBrief?>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public Handler(IOrdersRepository ordersRepository, IBooksRepository booksRepository, IMapper mapper)
        {
            _ordersRepository = Guard.Against.Null(ordersRepository);
            _booksRepository = Guard.Against.Null(booksRepository);
            _mapper = Guard.Against.Null(mapper);
        }

        public async Task<OrderBrief?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var order = new Order(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow));
            var orderBooks = new List<OrderedBook>(request.OrderedBooks.Count());

            foreach (var book in request.OrderedBooks)
            {
                if (book.Quantity <= 0)
                {
                    throw new InvalidQuantityException(book.Quantity);
                }

                var bookFromRepo = await _booksRepository.GetBookByIdAsync(book.BookId);
                if (bookFromRepo is null)
                {
                    throw new BookNotFoundException(book.BookId);
                }

                var orderBook = new OrderedBook(order.Id, order, bookFromRepo.Id, bookFromRepo, book.Quantity, bookFromRepo.Price);
                orderBooks.Add(orderBook);
            }

            var createdOrder = await _ordersRepository.CreateOrder(order, orderBooks, cancellationToken);
            var result = _mapper.Map<OrderBrief>(createdOrder);
            return result;
        }
    }
}

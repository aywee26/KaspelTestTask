using Ardalis.GuardClauses;
using KaspelTestTask.Application.Features.OrderService.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using MediatR;

namespace KaspelTestTask.Application.Features.OrderService.Commands;

public record CreateOrderCommand(IEnumerable<OrderedBookRequestBrief> OrderedBooks) : IRequest<Order?>
{
    public class Handler : IRequestHandler<CreateOrderCommand, Order?>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBooksRepository _booksRepository;

        public Handler(IOrdersRepository ordersRepository, IBooksRepository booksRepository)
        {
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
        }

        public async Task<Order?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var order = new Order(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow));
            var orderBooks = new List<OrderedBook>(request.OrderedBooks.Count());

            foreach (var book in request.OrderedBooks)
            {
                var bookFromRepo = await _booksRepository.GetBookByIdAsync(book.BookId);
                if (bookFromRepo is null)
                {
                    return null;
                }

                var orderBook = new OrderedBook(order.Id, order, bookFromRepo.Id, bookFromRepo, book.Quantity);
                orderBooks.Add(orderBook);
            }

            var result = await _ordersRepository.CreateOrder(order, orderBooks, cancellationToken);
            return result;
        }
    }
}

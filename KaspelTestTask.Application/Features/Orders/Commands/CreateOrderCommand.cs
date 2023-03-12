using Ardalis.GuardClauses;
using AutoMapper;
using KaspelTestTask.Application.Features.Orders.Models;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using KaspelTestTask.Domain.Exceptions;
using MediatR;

namespace KaspelTestTask.Application.Features.Orders.Commands;

public record CreateOrderCommand(IEnumerable<OrderedBookRequestDto> OrderedBooks) : IRequest<OrderDto>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrdersRepository ordersRepository, IBooksRepository booksRepository, IMapper mapper)
    {
        _ordersRepository = Guard.Against.Null(ordersRepository);
        _booksRepository = Guard.Against.Null(booksRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            var orderBook = new OrderedBook(order, bookFromRepo, book.Quantity, bookFromRepo.Price);
            orderBooks.Add(orderBook);
        }

        var createdOrder = await _ordersRepository.CreateOrderAsync(order, orderBooks, cancellationToken);
        var result = _mapper.Map<OrderDto>(createdOrder);
        return result;
    }
}
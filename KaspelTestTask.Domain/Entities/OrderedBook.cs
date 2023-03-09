using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class OrderedBook
{
    private OrderedBook()
    {
    }

    public OrderedBook(Guid orderId, Order order, Guid bookId, Book book, int quantity, decimal price)
    {
        OrderId = orderId;
        Order = Guard.Against.Null(order);
        BookId = bookId;
        Book = Guard.Against.Null(book);
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; } = default!;


    public Guid BookId { get; private set; }
    public Book Book { get; private set; } = default!;

    public int Quantity { get; private set; }

    public decimal Price { get; private set; }
}

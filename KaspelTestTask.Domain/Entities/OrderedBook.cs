using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class OrderedBook
{
    private OrderedBook()
    {
    }

    public OrderedBook(Order order, Book book, int quantity, decimal price)
    {
        Order = Guard.Against.Null(order);
        Book = Guard.Against.Null(book);
        Quantity = quantity;
        Price = price;
    }

    public Order Order { get; private set; } = default!;

    public Book Book { get; private set; } = default!;

    public int Quantity { get; private set; }

    public decimal Price { get; private set; }
}

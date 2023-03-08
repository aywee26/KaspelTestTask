using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class Order
{
    private Order()
    {
    }

    public Order(Guid id, DateTime orderDate)
    {
        Id = id;
        OrderDate = orderDate;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime OrderDate { get; private set; } = DateTime.UtcNow;

    public ICollection<OrderedBook> OrderedBooks { get; set; } = default!;
}

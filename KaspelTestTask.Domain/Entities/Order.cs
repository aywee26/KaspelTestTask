using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class Order
{
    private Order()
    {
    }

    public Order(Guid id, DateTime orderTime)
    {
        Id = id;
        OrderTime = orderTime;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime OrderTime { get; private set; } = DateTime.UtcNow;

    public ICollection<OrderedBook> OrderedBooks { get; private set; } = default!;
}

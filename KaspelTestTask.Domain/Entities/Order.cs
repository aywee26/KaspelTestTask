namespace KaspelTestTask.Domain.Entities;

public class Order
{
    public Order(Guid id, DateOnly orderDate)
    {
        Id = id;
        OrderDate = orderDate;
    }

    public Guid Id { get; private set; }

    public DateOnly OrderDate { get; private set; }

    public ICollection<OrderedBook> OrderedBooks { get; private set; } = new List<OrderedBook>();
}

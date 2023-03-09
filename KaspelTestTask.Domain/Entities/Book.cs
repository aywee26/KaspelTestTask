using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class Book
{
    private Book()
    {
    }

    public Book(Guid id, string title, string author, DateOnly publicationDate, decimal price)
    {
        Id = id;
        Title = Guard.Against.Null(title);
        Author = Guard.Against.Null(author);
        PublicationDate = publicationDate;
        Price = price;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = default!;

    public string Author { get; private set; } = default!;

    public DateOnly PublicationDate { get; private set; }

    public decimal Price { get; private set; }

    public ICollection<OrderedBook> OrderedBooks { get; set; } = default!;
}

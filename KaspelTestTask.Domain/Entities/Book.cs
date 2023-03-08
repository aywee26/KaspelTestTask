using Ardalis.GuardClauses;

namespace KaspelTestTask.Domain.Entities;

public class Book
{
    private Book()
    {
    }

    public Book(Guid id, string title, DateOnly publicationDate)
    {
        Id = id;
        Title = Guard.Against.Null(title);
        PublicationDate = publicationDate;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Title { get; private set; } = default!;

    public DateOnly PublicationDate { get; private set; } = default;

    public ICollection<OrderedBook> OrderedBooks { get; set; } = default!;
}

namespace KaspelTestTask.Domain.Exceptions;

public class BookNotFoundException : ArgumentException
{
    public BookNotFoundException(Guid bookId)
        : base($"Book is not found. ID: '{bookId}'")
    {  
    }
}

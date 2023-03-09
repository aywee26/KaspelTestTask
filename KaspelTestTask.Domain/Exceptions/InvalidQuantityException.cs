namespace KaspelTestTask.Domain.Exceptions;

public class InvalidQuantityException : ArgumentException
{
    public InvalidQuantityException(int quantity)
        : base($"Quantity cannot be equal or less then 0. Quantity: {quantity}")
    {
    }
}

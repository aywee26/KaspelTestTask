using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KaspelTestTask.Infrastructure.Converters;

/// <summary>
/// SQL Server provider doesn't support DateOnly, so this converter is used.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}

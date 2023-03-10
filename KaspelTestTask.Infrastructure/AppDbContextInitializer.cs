using KaspelTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;

    public AppDbContextInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task ApplyMigrationsAsync()
    {
        try
        {
            await TryApplyMigrationsAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch
        {
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Books.Any())
        {
            var books = new[]
            {
                new Book(id: Guid.NewGuid(), title: "Мастер и Маргарита", author: "Булгаков Михаил", publicationDate: DateOnly.FromDateTime(new DateTime(1973, 01, 01)), price: 100m),
                new Book(id: Guid.NewGuid(), title: "Собачье сердце", author: "Булгаков Михаил", publicationDate: DateOnly.FromDateTime(new DateTime(1987, 01, 01)), price: 100m),
                new Book(id: Guid.NewGuid(), title: "Триумфальная арка", author: "Ремарк Эрих", publicationDate: DateOnly.FromDateTime(new DateTime(1945, 01, 01)), price: 100m),
                new Book(id: Guid.NewGuid(), title: "Три товарища", author: "Ремарк Эрих", publicationDate: DateOnly.FromDateTime(new DateTime(1936, 12, 01)), price: 100m),
                new Book(id: Guid.NewGuid(), title: "12 стульев", author: "Ильф и Петров", publicationDate: DateOnly.FromDateTime(new DateTime(1928, 01, 01)), price: 100m)
            };

            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }
    }

    private async Task TryApplyMigrationsAsync()
    {
        await _context.Database.MigrateAsync();
    }
}

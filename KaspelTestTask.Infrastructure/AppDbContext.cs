using KaspelTestTask.Domain.Entities;
using KaspelTestTask.Infrastructure.Comparers;
using KaspelTestTask.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region DbSets
    public DbSet<Book> Books => Set<Book>();

    public DbSet<Order> Orders => Set<Order>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        var book = modelBuilder.Entity<Book>();
        book.HasKey(b => b.Id);
        book.Property(b => b.Title).IsRequired().HasMaxLength(200);
        book.Property(b => b.Author).IsRequired().HasMaxLength(200);
        book.Property(b => b.PublicationDate).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        book.Property(b => b.PublicationDate).IsRequired();
        book.Property(b => b.Price).IsRequired().HasPrecision(18, 4);
        book.HasIndex(b => b.Title);

        var order = modelBuilder.Entity<Order>();
        order.HasKey(ord => ord.Id);
        order.Property(ord => ord.OrderDate).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        order.Property(ord => ord.OrderDate).IsRequired();
        order.HasIndex(ord => ord.OrderDate);

        var orderedBook = modelBuilder.Entity<OrderedBook>();
        orderedBook.HasKey("OrderId", "BookId");
        orderedBook.HasOne(ob => ob.Order).WithMany(ord => ord.OrderedBooks).HasForeignKey("OrderId");
        orderedBook.Property(ob => ob.Quantity).IsRequired();
        orderedBook.Property(ob => ob.Price).IsRequired().HasPrecision(18, 4);

        base.OnModelCreating(modelBuilder);
    }
}

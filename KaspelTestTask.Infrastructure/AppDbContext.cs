﻿using KaspelTestTask.Domain.Entities;
using KaspelTestTask.Infrastructure.Comparers;
using KaspelTestTask.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.PublicationDate).HasConversion<DateOnlyConverter, DateOnlyComparer>();

        modelBuilder.Entity<Order>().HasKey(ord => ord.Id);
        modelBuilder.Entity<Order>().Property(ord => ord.OrderTime).IsRequired();

        modelBuilder.Entity<OrderedBook>()
            .HasKey(ob => new { ob.BookId, ob.OrderId });
        modelBuilder.Entity<OrderedBook>()
            .HasOne(ob => ob.Book)
            .WithMany(b => b.OrderedBooks)
            .HasForeignKey(ob => ob.BookId);
        modelBuilder.Entity<OrderedBook>()
            .HasOne(ob => ob.Order)
            .WithMany(o => o.OrderedBooks)
            .HasForeignKey(ob => ob.OrderId);


        base.OnModelCreating(modelBuilder);
    }
}

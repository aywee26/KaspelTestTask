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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Book>().Property(b => b.Title).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.PublicationDate).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        base.OnModelCreating(modelBuilder);
    }
}
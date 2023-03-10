using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _dbContext;

    public OrdersRepository(AppDbContext appDbContext)
    {
        _dbContext = Guard.Against.Null(appDbContext);
    }

    public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(Guid? id = null, DateOnly? orderDate = null, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbContext.Orders;

        if (id is not null)
        {
            query = query.Where(o => o.Id == id);
        }

        if (orderDate is not null)
        {
            query = query.Where(o => o.OrderDate.Equals(orderDate));
        }

        query = query.Include(o => o.OrderedBooks).ThenInclude(ob => ob.Book);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Order?> CreateOrderAsync(Order order, IEnumerable<OrderedBook> orderedBooks, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Orders.AddAsync(order, cancellationToken);
        order.OrderedBooks = orderedBooks.ToList();
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Orders
            .Where(o => o.Id == id)
            .Include(o => o.OrderedBooks)
            .ThenInclude(ob => ob.Book);

        return await query.FirstOrDefaultAsync();
    }
}

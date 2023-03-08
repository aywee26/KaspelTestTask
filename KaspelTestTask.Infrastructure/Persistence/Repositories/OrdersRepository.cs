using Ardalis.GuardClauses;
using KaspelTestTask.Application.Repositories.Abstractions;
using KaspelTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KaspelTestTask.Infrastructure.Persistence.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _dbContext;

    public OrdersRepository(AppDbContext appDbContext)
    {
        _dbContext = Guard.Against.Null(appDbContext);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(Guid? id = null, DateTime? orderDate = null, CancellationToken cancellationToken = default)
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

        return await query.ToListAsync(cancellationToken);
    }
}

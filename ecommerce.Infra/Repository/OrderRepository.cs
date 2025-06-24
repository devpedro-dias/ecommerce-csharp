using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infra.Repository;

public class OrderRepository : RepositoryBaseConfig<Order>, IOrderRepository
{
    protected readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<Order>> GetByUserIdAsync(string userId)
    {
        return _context.Order
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderProducts)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithProductsAndStockAsync(Guid orderId)
    {
        return await _context.Order
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.Stock)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}

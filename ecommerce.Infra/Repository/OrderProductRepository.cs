using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infra.Repository;

public class OrderProductRepository : RepositoryBaseConfig<OrderProduct>, IOrderProductRepository
{
    protected readonly AppDbContext _context;
    public OrderProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<OrderProduct> GetByIdAsync(Guid id)
    {
        return await _context.OrderProduct.Include(op => op.Product).Include(op => op.Order).FirstOrDefaultAsync(op => op.Id == id);
    }

}

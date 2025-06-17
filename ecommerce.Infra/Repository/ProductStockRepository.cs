using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infra.Repository;

public class ProductStockRepository : RepositoryBaseConfig<ProductStock>, IProductStockRepository
{
    protected readonly AppDbContext _context;
    public ProductStockRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<ProductStock>> GetAllAsync()
    {
        return await _context.ProductStock.Include(ps => ps.Product).ToListAsync();
    }

    public async Task<ProductStock> GetByIdAsync(Guid id)
    {
        return await _context.ProductStock.Include(ps => ps.Product).FirstOrDefaultAsync(ps => ps.Id == id);
    }

    public async Task<ProductStock> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductStock.Include(ps => ps.Product).FirstAsync(ps => ps.ProductId == productId);
    }
}

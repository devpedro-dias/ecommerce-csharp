using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Infra.Repository;

public class ProductRepository : RepositoryBaseConfig<Product>, IProductRepository
{
    protected readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Products
                    .Include(p => p.Stock)
                    .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Stock)
            .ToListAsync();
    }

}
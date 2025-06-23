using ecommerce.API.DTOs.Product;
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

    public async Task<bool> UpdateWithStockAsync(Guid id, Product product)
    {
        var existingProduct = await GetByIdAsync(id);
        if (existingProduct == null)
            return false;

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.UnitPrice = product.UnitPrice;

        if (existingProduct.Stock != null)
        {
            var existingStock = await _context.ProductStock
                .FirstOrDefaultAsync(s => s.ProductId == existingProduct.Id);

            if (existingStock != null)
            {
                existingStock.Quantity = product.Stock?.Quantity ?? 0;
            }
        }
        else if (product.Stock != null)
        {
            var newStock = new ProductStock
            {
                ProductId = existingProduct.Id,
                Quantity = product.Stock.Quantity
            };
            existingProduct.Stock = newStock;
            _context.ProductStock.Add(newStock);
        }

        _context.Products.Update(existingProduct);
        return await _context.SaveChangesAsync() > 0;
    }
}
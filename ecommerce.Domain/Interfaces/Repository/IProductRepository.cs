using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository.DbConfig;

namespace ecommerce.Domain.Interfaces.Repository;

public interface IProductRepository : IRepositoryBaseConfig<Product>
{
    Task<Product> GetByIdAsync(Guid id);
    Task<bool> UpdateWithStockAsync(Guid id, Product product);
}


using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository.DbConfig;

namespace ecommerce.Domain.Interfaces.Repository;

public interface IProductStockRepository : IRepositoryBaseConfig<ProductStock>
{
    Task<List<ProductStock>> GetAllAsync();
    Task<ProductStock> GetByIdAsync(Guid id);
}

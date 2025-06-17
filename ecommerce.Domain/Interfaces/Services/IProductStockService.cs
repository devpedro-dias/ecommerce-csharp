using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Services.DbConfig;

namespace ecommerce.Domain.Interfaces.Services;

public interface IProductStockService : IServiceBaseConfig<ProductStock>
{
    public Task<List<ProductStock>> GetAllAsync();
    public Task<ProductStock> GetByIdAsync(Guid id);
}

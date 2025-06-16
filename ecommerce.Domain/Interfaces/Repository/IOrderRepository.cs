using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository.DbConfig;

namespace ecommerce.Domain.Interfaces.Repository;

public interface IOrderRepository : IRepositoryBaseConfig<Order>
{
    Task<List<Order>> GetByUserIdAsync(string userId); 
}

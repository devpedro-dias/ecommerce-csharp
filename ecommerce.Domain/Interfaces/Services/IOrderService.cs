using ecommerce.API.DTOs.Request.OrderProducts;
using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Services.DbConfig;

namespace ecommerce.Domain.Interfaces.Services;

public interface IOrderService : IServiceBaseConfig<Order>
{
    public Task<Order> CreateOrderWithProductsAsync(string userId, DateTime date, List<OrderProductsRequestDTO> orderProducts);
    public Task<List<Order>> GetByUserIdAsync(string userId);
    Task<bool> DeleteOrderAndRestoreStockAsync(Guid orderId);
}

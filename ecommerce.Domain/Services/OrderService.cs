using ecommerce.API.DTOs.Request.OrderProducts;
using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Services.DbConfig;


namespace ecommerce.Domain.Services;

public class OrderService : ServiceBaseConfig<Order>, IOrderService
{
    protected readonly IOrderRepository _orderRepository;
    protected readonly IProductRepository _productRepository;
    //  protected readonly IStockRepository _stockRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    : base(orderRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }


    public async Task<Order> CreateOrderWithProductsAsync(string userId, DateTime date, List<OrderProductsRequestDTO> orderProducts)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Date = date,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderProducts = new List<OrderProduct>()
        };

        foreach (var item in orderProducts)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new Exception($"Product ID {item.ProductId} not found.");

            if (product.Stock.Quantity < item.Quantity)
                throw new Exception($"Stock is insufficient for: '{product.Name}'.");

            product.Stock.Quantity -= item.Quantity;
            await _productRepository.UpdateAsync(product);

            var orderProduct = new OrderProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = item.Quantity,
                TotalPrice = product.UnitPrice * item.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            order.OrderProducts.Add(orderProduct);
        }

        await _orderRepository.AddAsync(order);
        return order;
    }

    public Task<List<Order>> GetByUserIdAsync(string userId)
    {
        return _orderRepository.GetByUserIdAsync(userId);
    }
}

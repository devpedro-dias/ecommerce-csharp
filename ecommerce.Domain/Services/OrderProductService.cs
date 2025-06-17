using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Interfaces.Services.DbConfig;
using ecommerce.Domain.Services.DbConfig;

namespace ecommerce.Domain.Services;

public class OrderProductService : ServiceBaseConfig<OrderProduct>, IOrderProductService
{
    protected readonly IOrderProductRepository _orderProductRepository;

	public OrderProductService(IOrderProductRepository orderProductRepository) : base(orderProductRepository)
	{
		_orderProductRepository = orderProductRepository;
	}
}

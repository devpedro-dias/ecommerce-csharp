using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Services.DbConfig;

namespace ecommerce.Domain.Services;

public class ProductStockService : ServiceBaseConfig<ProductStock>, IProductStockService
{
    protected readonly IProductStockRepository _productStockRepository;
    public ProductStockService(IProductStockRepository productStockRepository) : base(productStockRepository)
    {
        _productStockRepository = productStockRepository;
    }
}

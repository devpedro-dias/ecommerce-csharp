using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Services.DbConfig;

namespace ecommerce.Domain.Services;

public class ProductService : ServiceBaseConfig<Product>, IProductService
{
    protected readonly IProductRepository _productRepository;

    public ProductService(IProductRepository repositoryBase) : base(repositoryBase)
    {
        _productRepository = repositoryBase;
    }
}

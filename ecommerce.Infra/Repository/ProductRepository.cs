using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;

namespace ecommerce.Infra.Repository;

public class ProductRepository : RepositoryBaseConfig<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
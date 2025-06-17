using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository.DbConfig;

namespace ecommerce.Infra.Repository;

public class OrderProductRepository : RepositoryBaseConfig<OrderProduct>, IOrderProductRepository
{
    protected readonly AppDbContext _context;
    public OrderProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}

using ecommerce.API.DTOs.Product;
using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Services.DbConfig;

namespace ecommerce.Domain.Interfaces.Services;

public interface IProductService : IServiceBaseConfig<Product>
{
    public Task<bool> UpdateAsync(Guid id, ProductRequestDTO productDto);
}

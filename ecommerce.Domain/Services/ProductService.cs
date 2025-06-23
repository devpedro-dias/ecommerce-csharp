using ecommerce.API.DTOs.Product;
using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Services.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Domain.Services;

public class ProductService : ServiceBaseConfig<Product>, IProductService
{
    protected readonly IProductRepository _productRepository;

    public ProductService(IProductRepository repositoryBase) : base(repositoryBase)
    {
        _productRepository = repositoryBase;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, ProductRequestDTO productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return false;

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.UnitPrice = productDto.UnitPrice;

        if (product.Stock != null)
        {
            product.Stock.Quantity = productDto.StockQuantity;
        }
        else
        {
            product.Stock = new ProductStock
            {
                ProductId = product.Id,
                Quantity = productDto.StockQuantity
            };
        }

        return await _productRepository.UpdateWithStockAsync(id, product);
    }
}

using ecommerce.API.DTOs.Product;
using ecommerce.API.DTOs.Response.Product;
using ecommerce.Domain.Entities;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Shared.DTOs.Response.Product;
using ecommerce.Shared.DTOs.Response.ProductStock;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDTO>>> Get()
    {
        var products = await _productService.GetAllAsync();
        var dtos = products.Select(p => new ProductResponseDTO(p.Id, p.Name, p.Description, p.UnitPrice)
        {
            Stock = new ProductStockSummaryResponseDTO(p.Stock?.Quantity ?? 0)
        }).ToList();

        return Ok(dtos);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDTO>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        var dto = new ProductResponseDTO(product.Id, product.Name, product.Description, product.UnitPrice)
        {
            Stock = new ProductStockSummaryResponseDTO(product.Stock?.Quantity ?? 0)
        };

        return Ok(dto);
    }


    [HttpPost]
    public async Task<ActionResult<ProductResponseDTO>> Create(ProductRequestDTO productDto)
    {
        var product = new Product(productDto.Name, productDto.Description, productDto.UnitPrice)
        {
            Stock = new ProductStock
            {
                Quantity = productDto.StockQuantity
            }
        };

        var newProduct = await _productService.AddAsync(product);

        var dto = new ProductResponseDTO(newProduct.Id, newProduct.Name, newProduct.Description, newProduct.UnitPrice)
        {
            Stock = new ProductStockSummaryResponseDTO(newProduct.Stock?.Quantity ?? 0)
        };

        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductRequestDTO productDto)
    {
        var product = new Product(productDto.Name, productDto.Description, productDto.UnitPrice)
        {
            Id = id
        };

        var updated = await _productService.UpdateAsync(product);
        if (!updated)
            return StatusCode(304, "Error on update product.");

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        await _productService.DeleteAsync(product);
        return NoContent();
    }
}
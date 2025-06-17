using ecommerce.Domain.Interfaces.Services;
using ecommerce.Shared.DTOs.Response.ProductStock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class ProductStockController : ControllerBase
{
    private IProductStockService _productStockService;

    public ProductStockController(IProductStockService productStockService)
    {
        _productStockService = productStockService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductStockResponseDTO>>> GetAll()
    {
        var products = await _productStockService.GetAllAsync();

        var dtos = products.Select(p => new ProductStockResponseDTO(
            p.Id,
            p.ProductId,
            p.Product?.Name,
            p.Product?.UnitPrice,
            p.Quantity
        )).ToList();

        return Ok(dtos);
    }

    [HttpGet("/productStock/{id}")]
    public async Task<ActionResult<ProductStockResponseDTO>> GetByProductStockId(Guid id)
    {
        var productStock = await _productStockService.GetByIdAsync(id);

        if(productStock == null) return NotFound();

        var dto = new ProductStockResponseDTO(
            productStock.Id,
            productStock.ProductId,
            productStock.Product?.Name,
            productStock.Product?.UnitPrice,
            productStock.Quantity);

        return Ok(dto);
    }

    [HttpGet("/productStock/product/{productId}")]
    public async Task<ActionResult<ProductStockResponseDTO>> GetByProductId(Guid productId)
    {
        var productStock = await _productStockService.GetAllAsync();
        var stock = productStock.FirstOrDefault(ps => ps.ProductId == productId);

        if(stock == null) return NotFound();

        var dto = new ProductStockResponseDTO(
            stock.Id,
            stock.ProductId,
            stock.Product?.Name,
            stock.Product?.UnitPrice,
            stock.Quantity
        );

        return Ok(dto);
    }
}

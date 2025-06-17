using ecommerce.API.DTOs.Response.OrderProducts;
using ecommerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class OrderProductController : ControllerBase
{
    private readonly IOrderProductService _orderProductService;

    public OrderProductController(IOrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderProductResponseDTO>>> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId)) return Unauthorized();

        var orderProducts = await _orderProductService.GetAllAsync();

        var dtos = orderProducts.Select(op => new OrderProductResponseDTO(
            op.Id,
            op.ProductId,
            op.Product?.Name,
            op.Product?.UnitPrice,
            op.OrderId,
            op.Order?.Date ?? default,
            op.Quantity,
            op.TotalPrice
        )).ToList();


        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderProductResponseDTO>> GetByOrderProductId(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId)) return Unauthorized();

        var orderProduct = await _orderProductService.GetByIdAsync(id);
        if (orderProduct == null) return NotFound();

        var dto = new OrderProductResponseDTO(
            orderProduct.Id,
            orderProduct.ProductId,
            orderProduct.Product?.Name,
            orderProduct.Product?.UnitPrice,
            orderProduct.OrderId,
            orderProduct.Order?.Date ?? default,
            orderProduct.Quantity,
            orderProduct.TotalPrice
        );

        return Ok(dto);
    }

}

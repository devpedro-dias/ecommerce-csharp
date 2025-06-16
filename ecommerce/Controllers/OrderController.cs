using ecommerce.API.DTOs.Request.Order;
using ecommerce.API.DTOs.Request.OrderProducts;
using ecommerce.API.DTOs.Response.Order;
using ecommerce.API.DTOs.Response.OrderProducts;
using ecommerce.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerce.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponseDTO>>> GetByUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId)) return Unauthorized();

        var orders = await _orderService.GetByUserIdAsync(userId);

        var dtos = orders.Select(order => new OrderResponseDTO(
            order.Id,
            order.UserId!,
            order.Date,
            order.OrderProducts.Select(op => new OrderProductResponseDTO(
                op.Id,
                op.ProductId,
                op.Quantity,
                op.TotalPrice
            )).ToList()
        )).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDTO>> GetByOrderId(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId)) return Unauthorized();

        var order = await _orderService.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        if (order.UserId != userId)
            return Forbid();

        var dto = new OrderResponseDTO(
            order.Id,
            order.UserId!,
            order.Date,
            order.OrderProducts.Select(op => new OrderProductResponseDTO(
                op.Id,
                op.ProductId,
                op.Quantity,
                op.TotalPrice
            )).ToList()
        );

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDTO>> Create([FromBody] OrderRequestDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId))
            return Unauthorized();

        var order = await _orderService.CreateOrderWithProductsAsync(userId, dto.Date, dto.OrderProducts);

        var response = new OrderResponseDTO(
            order.Id,
            order.UserId!,
            order.Date,
            order.OrderProducts.Select(op => new OrderProductResponseDTO(
                op.Id,
                op.ProductId,
                op.Quantity,
                op.TotalPrice
            )).ToList()
        );

        return CreatedAtAction(nameof(GetByOrderId), new { id = order.Id }, response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(userId)) return Unauthorized();

        var order = await _orderService.GetByIdAsync(id);

        if (order == null) return NotFound();

        if (order.UserId != userId) return Forbid();

        await _orderService.DeleteAsync(order);

        return NoContent();
    }
}

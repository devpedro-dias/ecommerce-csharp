using ecommerce.API.DTOs.Response.OrderProducts;

namespace ecommerce.API.DTOs.Response.Order;

public record OrderResponseDTO(Guid Id, string UserId, DateTime Date, List<OrderProductResponseDTO> OrderProduct)
{
}

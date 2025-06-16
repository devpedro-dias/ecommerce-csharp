namespace ecommerce.API.DTOs.Response.OrderProducts;

public record OrderProductResponseDTO(Guid Id, Guid ProductId, int Quantity, decimal TotalPrice);

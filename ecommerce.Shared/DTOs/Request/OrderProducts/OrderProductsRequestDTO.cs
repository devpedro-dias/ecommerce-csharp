namespace ecommerce.API.DTOs.Request.OrderProducts;

public record OrderProductsRequestDTO(
    Guid ProductId,
    int Quantity
);
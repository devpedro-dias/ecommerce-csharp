namespace ecommerce.API.DTOs.Response.OrderProducts;

public record OrderProductResponseDTO(
    Guid Id,
    Guid ProductId,
    string? ProductName,
    decimal? ProductUnitPrice,
    Guid OrderId,
    DateTime OrderDate,
    int Quantity,
    decimal TotalPrice
);


namespace ecommerce.Shared.DTOs.Response.ProductStock;

public record ProductStockResponseDTO(
    Guid Id,
    Guid ProductId,
    string? ProductName,
    decimal? ProductUnitPrice,
    int Quantity
);

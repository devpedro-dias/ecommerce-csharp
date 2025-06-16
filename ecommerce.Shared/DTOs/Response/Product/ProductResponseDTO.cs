using ecommerce.Shared.DTOs.Response.ProductStock;

namespace ecommerce.API.DTOs.Response.Product;

public record ProductResponseDTO(Guid Id, string Name, string Description, decimal UnitPrice)
{
    public ProductStockResponseDTO Stock { get; init; }
}

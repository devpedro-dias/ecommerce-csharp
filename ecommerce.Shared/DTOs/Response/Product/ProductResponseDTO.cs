using ecommerce.Shared.DTOs.Response.Product;

namespace ecommerce.API.DTOs.Response.Product;

public record ProductResponseDTO(Guid Id, string Name, string Description, decimal UnitPrice)
{
    public ProductStockSummaryResponseDTO Stock { get; init; }
}

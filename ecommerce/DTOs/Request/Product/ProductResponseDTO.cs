namespace ecommerce.API.DTOs.Request.Product
{
    public record ProductResponseDTO(Guid Id, string Name, string Description, decimal UnitPrice)
    {
    }
}

using ecommerce.API.DTOs.Response.OrderProducts;
using ecommerce.Shared.DTOs.Response.Order;
using System.Text.Json.Serialization;

namespace ecommerce.API.DTOs.Response.Order;

public record OrderResponseDTO(
    Guid Id,
    string UserId,
    DateTime Date, 
    [property: JsonPropertyName("orderProduct")]
    List<OrderProductSummaryDTO> OrderProducts
    );

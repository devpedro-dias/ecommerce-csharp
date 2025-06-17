namespace ecommerce.Shared.DTOs.Response.Order;
public record OrderProductSummaryDTO(
    Guid Id,
    Guid ProductId,
    Guid OrderId,
    int Quantity,
    decimal TotalPrice
);


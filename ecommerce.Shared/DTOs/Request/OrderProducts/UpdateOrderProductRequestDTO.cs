namespace ecommerce.API.DTOs.Request.OrderProducts;

public record UpdateOrderProductRequestDTO(int Quantity, decimal TotalPrice, DateTime Date)
{
}

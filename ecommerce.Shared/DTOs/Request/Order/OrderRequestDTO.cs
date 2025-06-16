using ecommerce.API.DTOs.Request.OrderProducts;

namespace ecommerce.API.DTOs.Request.Order
{
    public record OrderRequestDTO(
        DateTime Date,
        List<OrderProductsRequestDTO> OrderProducts
    );
}
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Front.Models;

public class OrderItemFormModel
{
    [Required(ErrorMessage = "Product is required.")]
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; } = 1;

    public decimal Subtotal => UnitPrice * Quantity;
    
}

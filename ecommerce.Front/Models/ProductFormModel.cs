namespace ecommerce.Front.Models;
public class ProductFormModel
{
    public string Product { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
}

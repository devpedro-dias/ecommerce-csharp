namespace ecommerce.Domain.Entities;

public class OrderProduct
{
    public Guid Id {  get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null;
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = new Order();

    public OrderProduct()
    {
    }

}
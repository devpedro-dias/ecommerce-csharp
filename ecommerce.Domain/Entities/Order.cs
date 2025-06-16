using ecommerce.Domain.Entities;

namespace ecommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
    public string? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public Order()
    {
    }
}

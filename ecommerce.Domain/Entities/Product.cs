using System.ComponentModel.DataAnnotations;

namespace ecommerce.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ProductStock Stock { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public Product(string name, string description, decimal unitPrice)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
        }

        public Product() { }
    }
}

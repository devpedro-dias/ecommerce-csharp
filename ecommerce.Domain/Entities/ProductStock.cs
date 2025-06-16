using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Domain.Entities
{
    public class ProductStock
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }

        public ProductStock() { }

        public ProductStock(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}

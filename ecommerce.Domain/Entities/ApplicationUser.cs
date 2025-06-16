using ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public ApplicationUser()
    {
    }
}
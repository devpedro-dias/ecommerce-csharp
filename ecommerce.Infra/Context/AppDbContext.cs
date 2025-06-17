using ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ecommerce.Infra.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }
    public DbSet<ProductStock> ProductStock { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EcommerceV0;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(connectionString)
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasPrecision(10, 2);

        builder.Entity<Product>()
            .HasOne(p => p.Stock)
            .WithOne(s => s.Product)
            .HasForeignKey<ProductStock>(s => s.ProductId);

        builder.Entity<OrderProduct>()
            .Property(p => p.TotalPrice)
            .HasPrecision(10, 2);

        builder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        builder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.ProductId);

        builder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.OrderId);
    }
}

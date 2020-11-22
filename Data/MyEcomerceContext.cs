using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jairoEcomerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jairoEcomerce.Data
{
  public class MyEcomerceContext : DbContext
  {
    public MyEcomerceContext(DbContextOptions<MyEcomerceContext> options): base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
            builder.Entity<Order>()
                      .HasData(new Order() 
                      {
                          Id = 1,
                          OrderDate = DateTime.UtcNow,
                          OrderNumber = "9876"
                      });

      builder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");

      builder.Entity<OrderItem>()
        .Property(p => p.UnitPrice)
        .HasColumnType("decimal(18,2)");
    }
  }
}

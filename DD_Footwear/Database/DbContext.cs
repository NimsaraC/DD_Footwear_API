using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

public class DDShopDbContext : DbContext
{
    public DDShopDbContext(DbContextOptions<DDShopDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockItems> StockItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Stock>().HasMany(c => c.StockItems).WithOne().HasForeignKey(c => c.StockId);

        modelBuilder.Entity<Order>()
                .HasMany(c => c.items)
                .WithOne()
                .HasForeignKey(ci => ci.OrderId);
    }
}

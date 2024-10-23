using DD_Footwear.Models;
using Microsoft.EntityFrameworkCore;

public class DDShopDbContext : DbContext
{
    public DDShopDbContext(DbContextOptions<DDShopDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}

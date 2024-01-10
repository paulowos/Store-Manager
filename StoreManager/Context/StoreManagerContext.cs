using Microsoft.EntityFrameworkCore;
using StoreManager.Model;

namespace StoreManager.Context;

public class StoreManagerContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<SaleProduct> SalesProducts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ??
                               "Server=localhost;Database=StoreManager;User=sa;Password=Password!123";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SaleProduct>().HasKey(saleProduct => new { saleProduct.SaleId, saleProduct.ProductId });
    }
}
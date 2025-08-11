using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using Models;
using Microsoft.EntityFrameworkCore;


namespace EFCoreBasicsImplementation.Data;

public class ProductAppContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductReview> ProductReviews { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductAppContext).Assembly);
    }
}


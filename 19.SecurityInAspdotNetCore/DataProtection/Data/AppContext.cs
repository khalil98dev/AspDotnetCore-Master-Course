using DataProtection.Data.Configurations;
using DataProtection.Modules;
using Microsoft.EntityFrameworkCore;

namespace DataProtection.Data;

public class AppContext : DbContext
{

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }

    public DbSet<Bid> Bids => Set<Bid>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BidConfigurations).Assembly);
    }

}


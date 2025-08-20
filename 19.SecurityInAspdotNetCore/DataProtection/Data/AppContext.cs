using DataProtection.Data.Configurations;
using DataProtection.Modules;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataProtection.Data;

public class AppContext(DbContextOptions<AppContext> options) 
: DbContext(options),IDataProtectionKeyContext
{
    public DbSet<Bid> Bids => Set<Bid>();

    public DbSet<DataProtectionKey> DataProtectionKeys =>Set<DataProtectionKey>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BidConfigurations).Assembly);
    }

}


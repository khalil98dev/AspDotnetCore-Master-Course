
using DataProtection.Modules;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;

namespace DataProtection.Data.Configurations;

public class BidConfigurations : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(p => p.ID)
                .HasName("BidID");

        builder.ToTable("Bids");


        builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Firstname");

        builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Lastname");
        builder.Property(p => p.Address)
                 .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Address");
        builder.Property(p => p.Ammount)
                .IsRequired()
                .HasColumnName("Ammount")
                .HasColumnType("decimal(18,2)")
                ;

        builder.Property(p => p.BidDate)
                .HasColumnName("BidDate");
                

        builder.Property(p => p.Phone)
                .HasColumnName("Phone")
                .IsRequired(false);
        builder.Property(p => p.Email)
                .HasColumnName("Email")
                .IsRequired(false);       
    }
}
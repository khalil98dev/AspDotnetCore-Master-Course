
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;   

namespace EFCoreBasicsImplementation.Data.Configurations;

public class ProductreviewConfigurations : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> review)
    {
        review.HasKey(r => r.Id);

        review.Property(r => r.ProductId).IsRequired(); 
        review.Property(r => r.Reviewer)
            .IsRequired()
            .HasMaxLength(100);
        review.Property(r => r.Stars)
            .IsRequired()
            .HasColumnType("smallint"); 

        review.HasData(
            new ProductReview
            {
                Id = Guid.Parse("ddd4e07a-4772-47f7-9cba-6bfc07c26bfe"),
                ProductId = Guid.Parse("2779ee47-10b0-4bd7-8342-404006aa1392"),
                Reviewer = "John Doe",
                Stars = 4
            },
            new ProductReview
            {
                Id = Guid.Parse("c30d9647-1603-4948-8266-88a850547be0"),
                ProductId = Guid.Parse("2779ee47-10b0-4bd7-8342-404006aa1392"),
                Reviewer = "Sarah Peter",
                Stars = 3
            }
            );
           
    }
}



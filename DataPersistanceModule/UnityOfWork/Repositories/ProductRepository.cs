using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using EFCoreBasicsImplementation.Data;
using EFCoreBasicsImplementation.interfaces;

namespace EFCoreBasicsImplementation.Repositories;

public class ProductRepository(ProductAppContext context) : IProductRepository
{


    public async Task<int> GetProductsCountAsync(CancellationToken ct) =>
    await context.Products.CountAsync(ct);

    public async Task<List<Product>> GetProductsPageAsync(CancellationToken ct,int page = 1, int pageSize = 10)
    {
        var products = await context.Products.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(ct);

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId,CancellationToken ct)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId,ct);

        if (product is null)
            return null;

        return product;
    }

    public async Task<List<ProductReview>> GetProductReviewsAsync(Guid productId,CancellationToken ct)
    {
        return await context.ProductReviews.Where(r => r.ProductId == productId).ToListAsync(ct);
    }

    public async Task<ProductReview?> GetReviewAsync(Guid productId, Guid reviewId, CancellationToken ct)
    {
        return await context.ProductReviews.
        FirstOrDefaultAsync(r => r.ProductId == productId && r.Id == reviewId,ct);
    }

    public  void AddProduct(Product product)
    {
        if (product is null)
            return ;
        context.Products.Add(product);
    }

    public async Task AddProductReviewAsync(ProductReview review,CancellationToken ct)
    {
       var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == review.ProductId, ct);

        if (product is null)
            throw new InvalidOperationException("Product not found");


        context.ProductReviews.Add(review);


        var reviews =  context.ProductReviews
            .Where(r => r.ProductId == review.ProductId).ToList();
            

        if (reviews.Count == 0)
            product.AverageRating = 0;
        else     
            product.AverageRating =(decimal) Math.Round(reviews.Average(r=>r.Stars),1,MidpointRounding.AwayFromZero); 

    }

    public async Task UpdateProductAsync(Product updatedProduct,CancellationToken ct)
    {
        var existingProduct =await context.Products.
        FirstOrDefaultAsync(p => p.Id == updatedProduct.Id,ct);

        if (existingProduct is null)
            throw new InvalidOperationException("Product not found");

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price; 
    }

    public async Task DeleteProductAsync(Guid id,CancellationToken ct)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id,ct);
        if (product == null)
            throw new InvalidOperationException("Product not found");

        context.Products.Remove(product);
    }
    public async Task<bool> ExistsByIdAsync(Guid id,CancellationToken ct) => await context.Products.AnyAsync(p => p.Id == id,ct);

    public async Task<bool> ExistsByNameAsync(string? name,CancellationToken ct)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        return await context.Products.AnyAsync(p => EF.Functions.Like(p.Name!.ToUpper(), name.ToUpper()),ct);
    }
}
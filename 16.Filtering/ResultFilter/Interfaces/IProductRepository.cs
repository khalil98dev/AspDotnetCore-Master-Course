using Models;

namespace EFCoreBasicsImplementation.Data;

public interface IProductRepository
{
    Task<bool> AddProductAsync(Product product,CancellationToken ct);
    Task<bool> AddProductReviewAsync(ProductReview review,CancellationToken ct);
    Task<bool> DeleteProductAsync(Guid id,CancellationToken ct);
    Task<bool> ExistsByIdAsync(Guid id,CancellationToken ct);
    Task<bool> ExistsByNameAsync(string? name,CancellationToken ct);
    Task<Product?> GetProductByIdAsync(Guid productId,CancellationToken ct);
    Task<List<ProductReview>> GetProductReviewsAsync(Guid productId,CancellationToken ct);
    Task<int> GetProductsCountAsync(CancellationToken ct);
    Task<List<Product>> GetProductsPageAsync(CancellationToken ct,int page = 1, int pageSize = 10);
    Task<ProductReview?> GetReviewAsync(Guid productId, Guid reviewId,CancellationToken ct);
    Task<bool> UpdateProductAsync(Product updatedProduct,CancellationToken ct);
}

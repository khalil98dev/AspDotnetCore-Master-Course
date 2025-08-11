using Models;

using EFCoreBasicsImplementation.Data; 


namespace EFCoreBasicsImplementation.interfaces;

public interface IProductRepository
{
    void AddProduct(Product product);
    Task AddProductReviewAsync(ProductReview review, CancellationToken ct);
    Task DeleteProductAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByNameAsync(string? name, CancellationToken ct);
    Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken ct);
    Task<List<ProductReview>> GetProductReviewsAsync(Guid productId, CancellationToken ct);
    Task<int> GetProductsCountAsync(CancellationToken ct);
    Task<List<Product>> GetProductsPageAsync(CancellationToken ct, int page = 1, int pageSize = 10);
    Task<ProductReview?> GetReviewAsync(Guid productId, Guid reviewId, CancellationToken ct);
    Task UpdateProductAsync(Product updatedProduct, CancellationToken ct);
}
